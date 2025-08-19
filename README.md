# box-script
A PowerShell replacement but using JavaScript syntax - Written in C#

# Examples
Checkout the [Examples Directory](src/BoxScript.Cli/Examples) for examples.

# Executing a script:

To execute an inline script you can just pass the inline script to the exe:
```bash
box-script.exe import { logger } from 'modules'; logger.Info('Hello world!');
```

To execute a script file you can pass the file path to the exe:
```bash
box-script.exe execute -f path/to/script.js
```

For more help, you can checkout [the execute verb options](src/BoxScript.Cli/Verbs/ExecuteVerb.cs) for the options or use the help command:
```bash
box-script.exe help
```

# IntelliSense support for JS scripts
There is an [index.d.ts](src/BoxScript.Cli/Examples/index.d.ts) file included in the project that provides IntelliSense support for all of the modules.

You can also generate a new `index.d.ts` file by running the following command:
```bash
box-script.exe generate
```

To get IntelliSense to work for the script while in VS2022 or VSCode, you just need to open the `index.d.ts` in another tab and then open your script file. Then IntelliSense should work automatically.

# Feature List
- [x] Basic arithmetic operations
- [x] Script Parsing & Execution
- [x] File IO
	- [x] System.IO.Path Parity
	- [x] System.IO.File Parity
	- [x] System.IO.Directory Parity
	- [x] Zip Compress/Decompress
	- [x] Stream proxies for Reader/Writer
- [ ] Database Support
	- [x] Common Providers (MSSQL, MySQL, PostgreSQL, SQLite) 
	- [x] Generate Operations (CRUD)
	- [ ] SQL Management Objects (SMO) for MSSQL
	- [ ] BulkInsert Operations
	- [ ] DB transfer operations
- [ ] CSV support
	- [x] Reading
	- [ ] Writing (Partial)
- [x] JSON support
- [x] HTTP request support
	- [x] File Upload (w/ Progress tracking)
	- [x] File Download (w/ Progress tracking)
	- [x] Basic GET, POST, PUT, DELETE
	- [x] Extensible settings for parameters, headers, etc.
- [x] JSDoc Generation
	- [x] Enum Classes 
	- [x] Class Definitions
	- [x] Module exports
- [x] Logging support
- [ ] Excel Support
	- [ ] Reading
	- [ ] Writing
