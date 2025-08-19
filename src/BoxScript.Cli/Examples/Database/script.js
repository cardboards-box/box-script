import { config, logger, db, file } from 'modules';

//Get the database connection string from the config
const connectionString = config.Get('Database:ConnectionString') || 'Data Source=database.db;';

//Delete the database if it already exists
if (file.Exists('database.db'))
	file.Delete('database.db');

const con = await db.SQLite()
    .WithConnection(connectionString)
	.Connect();

await con.Execute(`CREATE TABLE IF NOT EXISTS users (
	id INTEGER PRIMARY KEY,
	user_name TEXT NOT NULL,
	discriminator TEXT NOT NULL,	
	first_name TEXT NOT NULL,
	last_name TEXT NOT NULL,
	
	created_at TEXT NOT NULL DEFAULT CURRENT_TIMESTAMP,
	updated_at TEXT NOT NULL DEFAULT CURRENT_TIMESTAMP,

	UNIQUE (user_name, discriminator)
)`);

logger.Info('Database initialized successfully');

const insert = `INSERT INTO users (user_name, discriminator, first_name, last_name) 
VALUES (@username, @discriminator, @firstName, @lastName)`;

const inserts = [
	{ username: 'john_doe', discriminator: '1234', firstName: 'John', lastName: 'Doe' },
	{ username: 'jane_doe', discriminator: '5678', firstName: 'Jane', lastName: 'Doe' },
    { username: 'alice_smith', discriminator: '9101', firstName: 'Alice', lastName: 'Smith' },
    { username: 'bob_johnson', discriminator: '1122', firstName: 'Bob', lastName: 'Johnson' }
];

for (const user of inserts) {
	const settings = con.Settings()
		.AddParameters(user);
	await con.Execute(insert, settings);
    logger.Info('Inserted user: {username}', user.username);
}

const users = await con.Query("SELECT * FROM users");
for (const user of users) {
	logger.Info('User: {user}', JSON.stringify(user, null, 2));
}

const reader = await con.Multiple(`
SELECT * FROM users;
SELECT user_name as UserName, first_name as FirstName, last_name as LastName FROM users;`);

const basic = await reader.Next();
logger.Info('Basic User Data: {data}', JSON.stringify(basic, null, 2));

const modified = await reader.Next();
logger.Info('Modified User Data: {data}', JSON.stringify(modified, null, 2));

reader.Dispose();

con.Dispose();