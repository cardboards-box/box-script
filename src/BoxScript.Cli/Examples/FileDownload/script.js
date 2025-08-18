import { http, logger } from 'modules';

//The URL of the large file to download
const file = 'https://ash-speed.hetzner.com/1GB.bin';
//Get the file-name from the URL
const fileName = (() => {
    const parts = file.split('/');
    return parts[parts.length - 1];
})();
//Log the download progress every 5 seconds
const settings = http.Settings()
    .LogDownloads(true, 5);
//Trigger the download
const response = await http.Get(file, settings);
//Throw an error if the download failed
await response.ThrowIfBad(); 
//Save the file to the current directory
await response.ToFile(fileName); 
//Log the success message
logger.Info('File downloaded successfully to {fileName}', fileName);