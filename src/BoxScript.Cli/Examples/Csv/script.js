import { db, logger, csv, file } from 'modules';

const DATABASE_PATH = 'database.db';
const INSERT_SQL = `
INSERT INTO users (username, firstName, lastName) 
VALUES (@username, @firstName, @lastName)`;

async function createDb() {
    if (file.Exists(DATABASE_PATH))
        file.Delete(DATABASE_PATH);

    const conString = `Data Source=${DATABASE_PATH};`;
    const con = await db.SQLite()
        .WithConnection(conString)
		.Connect();

	logger.Debug('Database connected to!');

	await con.Execute(`CREATE TABLE IF NOT EXISTS users (
		id INTEGER PRIMARY KEY,
		username TEXT NOT NULL,
		firstName TEXT NOT NULL,
		lastName TEXT NOT NULL,
	
		createdAt TEXT NOT NULL DEFAULT CURRENT_TIMESTAMP,
		updatedAt TEXT NOT NULL DEFAULT CURRENT_TIMESTAMP,

		UNIQUE (username)
	)`);

	logger.Debug('Table created!');
	const inserts = [
		{ username: 'john_doe', firstName: 'John', lastName: 'Doe' },
		{ username: 'jane_doe', firstName: 'Jane', lastName: 'Doe' },
		{ username: 'alice_smith', firstName: 'Alice', lastName: 'Smith' },
		{ username: 'bob_johnson', firstName: 'Bob', lastName: 'Johnson' }
	];

	for (const user of inserts) {
		const settings = con.Settings().AddParameters(user);
		await con.Execute(INSERT_SQL, settings);
	}

	logger.Debug('Database populated!');
	return con;
}

//Get the database connection
const database = await createDb();
logger.Info('Database connected to!');

//Read the CSV of new users
const newUsers = await csv.Read('new-users.csv').RecordsAsync();
logger.Info('New Users: {data}', JSON.stringify(newUsers, null, 2));

//Add all of the new users to the database
for (const user of newUsers) {
	await database.Execute(INSERT_SQL, database.Settings().AddParameters(user));
	logger.Info('Added new user: {username}', user.username);
}

//Fetch all of the users from the database
const allUsers = await database.Query('SELECT * FROM users');
//Write all of the users to a CSV file
csv.Write('all-users.csv').WriteRecords(allUsers);
logger.Info('All users exported!');

//Close our database connection
database.Dispose();