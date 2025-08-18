import { http, logger, file, config } from 'modules';

//The base settings for any request to api.weather.gov
const settings = http.Settings()
    .SetBaseUrl('https://api.weather.gov')
    .SetHeader('User-Agent', '(BoxScript Client)');

/**
 * Formats the date to a string in the format YYYY-MM-DD HH:MM
 * @param {Date} date
 * @returns {string}
 */
function formateDate(date) {
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');
    const hours = String(date.getHours()).padStart(2, '0');
    const minutes = String(date.getMinutes()).padStart(2, '0');
    return `${year}-${month}-${day} ${hours}:${minutes}`;
}

/**
 * Gets the configuration from the config file
 * @returns {{ lat: number, long: number, filePath: string }}
 */
function getConfig() {
    const lat = parseFloat(config.Get('Weather:Lat') || '39.7636');
    const long = parseFloat(config.Get('Weather:Long') || '-84.1926');
    const filePath = config.Get('Weather:Output') || 'forecast.txt';
    return { lat, long, filePath };
}

/**
 * Fetch the grid points for the given lat and long from the API
 * @param {number} lat
 * @param {number} long
 * @returns {Promise<{gridX: number, gridY: number, gridId: string, city: string, state: string, name: string}>}
 */
async function fetchGridPoints(lat, long) {
    const points = await http.GetJson(`points/${lat},${long}`, settings);
    const gridX = points.properties.gridX,
        gridY = points.properties.gridY,
        gridId = points.properties.gridId;
    const { city, state } = points.properties.relativeLocation.properties;
    return {
        gridX, gridY,
        gridId,
        city, state,
        name: `${city}, ${state}`,
    }
}

/**
 * Fetches the forecast from the API
 * @param {string} gridId
 * @param {number} gridX
 * @param {number} gridY
 * @returns {Promise<Array<{name: string, startTime: string, endTime: string, detailedForecast: string}>>}
 */
async function fetchForecast(gridId, gridX, gridY) {
    const forecast = await http.GetJson(`gridpoints/${gridId}/${gridX},${gridY}/forecast`, settings);
    return forecast.properties.periods;
}

async function main() {
    const { lat, long, filePath } = getConfig();
    const location = await fetchGridPoints(lat, long);
    const forecast = await fetchForecast(location.gridId, location.gridX, location.gridY);

    //Open a file stream to write to
    const io = file.Create(filePath);
    //Write the header line to the file
    const header = `Weather Forecast for ${location.name} (${location.gridId}: ${location.gridX},${location.gridY})`;
    await io.WriteLineAsync(header);
    logger.Info(header);
    //Write the forecast to the file
    for (const period of forecast) {
        const start = formateDate(new Date(period.startTime));
        const end = formateDate(new Date(period.endTime));
        const line = `${period.name}: (${start} to ${end}): ${period.detailedForecast}`;
        await io.WriteLineAsync(line);
        logger.Info(line);
    }

    await io.FlushAsync();
    io.Dispose();
}

await main();