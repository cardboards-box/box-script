// This file is auto-generated from the loaded modules
// You should avoid modifying this file directly
// Generated: 2025-08-18 17:22 UTC

/** Specifies values that indicate whether a compression operation emphasizes speed or compression size. */
declare class CompressionLevel {
	/**
	 * The compression operation should optimally balance compression speed and output size. - 0
	 * @type {number}
	 */
	static get Optimal(): number;
	/**
	 * The compression operation should complete as quickly as possible, even if the resulting file is not optimally compressed. - 1
	 * @type {number}
	 */
	static get Fastest(): number;
	/**
	 * No compression should be performed on the file. - 2
	 * @type {number}
	 */
	static get NoCompression(): number;
	/**
	 * The compression operation should create output as small as possible, even if the operation takes a longer time to complete. - 3
	 * @type {number}
	 */
	static get SmallestSize(): number;
}

declare class SearchOption {
	static get TopDirectoryOnly(): number;
	static get AllDirectories(): number;
}

/** Specifies the data type of a field, a property, or a <see langword="Parameter" /> object of a .NET data provider. */
declare class DbType {
	/**
	 * A variable-length stream of non-Unicode characters ranging between 1 and 8,000 characters. - 0
	 * @type {number}
	 */
	static get AnsiString(): number;
	/**
	 * A variable-length stream of binary data ranging between 1 and 8,000 bytes. - 1
	 * @type {number}
	 */
	static get Binary(): number;
	/**
	 * An 8-bit unsigned integer ranging in value from 0 to 255. - 2
	 * @type {number}
	 */
	static get Byte(): number;
	/**
	 * A simple type representing Boolean values of <see langword="true" /> or <see langword="false" />. - 3
	 * @type {number}
	 */
	static get Boolean(): number;
	/**
	 * A currency value ranging from -2 63 (or -922,337,203,685,477.5808) to 2 63 -1 (or +922,337,203,685,477.5807) with an accuracy to a ten-thousandth of a currency unit. - 4
	 * @type {number}
	 */
	static get Currency(): number;
	/**
	 * A type representing a date value. - 5
	 * @type {number}
	 */
	static get Date(): number;
	/**
	 * A type representing a date and time value. - 6
	 * @type {number}
	 */
	static get DateTime(): number;
	/**
	 * A simple type representing values ranging from 1.0 x 10 -28 to approximately 7.9 x 10 28 with 28-29 significant digits. - 7
	 * @type {number}
	 */
	static get Decimal(): number;
	/**
	 * A floating point type representing values ranging from approximately 5.0 x 10 -324 to 1.7 x 10 308 with a precision of 15-16 digits. - 8
	 * @type {number}
	 */
	static get Double(): number;
	/**
	 * A globally unique identifier (or GUID). - 9
	 * @type {number}
	 */
	static get Guid(): number;
	/**
	 * An integral type representing signed 16-bit integers with values between -32768 and 32767. - 10
	 * @type {number}
	 */
	static get Int16(): number;
	/**
	 * An integral type representing signed 32-bit integers with values between -2147483648 and 2147483647. - 11
	 * @type {number}
	 */
	static get Int32(): number;
	/**
	 * An integral type representing signed 64-bit integers with values between -9223372036854775808 and 9223372036854775807. - 12
	 * @type {number}
	 */
	static get Int64(): number;
	/**
	 * A general type representing any reference or value type not explicitly represented by another <see langword="DbType" /> value. - 13
	 * @type {number}
	 */
	static get Object(): number;
	/**
	 * An integral type representing signed 8-bit integers with values between -128 and 127. - 14
	 * @type {number}
	 */
	static get SByte(): number;
	/**
	 * A floating point type representing values ranging from approximately 1.5 x 10 -45 to 3.4 x 10 38 with a precision of 7 digits. - 15
	 * @type {number}
	 */
	static get Single(): number;
	/**
	 * A type representing Unicode character strings. - 16
	 * @type {number}
	 */
	static get String(): number;
	/**
	 * A type representing a SQL Server <see langword="DateTime" /> value. If you want to use a SQL Server <see langword="time" /> value, use <see cref="F:System.Data.SqlDbType.Time" />. - 17
	 * @type {number}
	 */
	static get Time(): number;
	/**
	 * An integral type representing unsigned 16-bit integers with values between 0 and 65535. - 18
	 * @type {number}
	 */
	static get UInt16(): number;
	/**
	 * An integral type representing unsigned 32-bit integers with values between 0 and 4294967295. - 19
	 * @type {number}
	 */
	static get UInt32(): number;
	/**
	 * An integral type representing unsigned 64-bit integers with values between 0 and 18446744073709551615. - 20
	 * @type {number}
	 */
	static get UInt64(): number;
	/**
	 * A variable-length numeric value. - 21
	 * @type {number}
	 */
	static get VarNumeric(): number;
	/**
	 * A fixed-length stream of non-Unicode characters. - 22
	 * @type {number}
	 */
	static get AnsiStringFixedLength(): number;
	/**
	 * A fixed-length string of Unicode characters. - 23
	 * @type {number}
	 */
	static get StringFixedLength(): number;
	/**
	 * A parsed representation of an XML document or fragment. - 25
	 * @type {number}
	 */
	static get Xml(): number;
	/**
	 * Date and time data. Date value range is from January 1,1 AD through December 31, 9999 AD. Time value range is 00:00:00 through 23:59:59.9999999 with an accuracy of 100 nanoseconds. - 26
	 * @type {number}
	 */
	static get DateTime2(): number;
	/**
	 * Date and time data with time zone awareness. Date value range is from January 1,1 AD through December 31, 9999 AD. Time value range is 00:00:00 through 23:59:59.9999999 with an accuracy of 100 nanoseconds. Time zone value range is -14:00 through +14:00. - 27
	 * @type {number}
	 */
	static get DateTimeOffset(): number;
}

/** Specifies the type of a parameter within a query relative to the <see cref="T:System.Data.DataSet" />. */
declare class ParameterDirection {
	/**
	 * The parameter is an input parameter. - 1
	 * @type {number}
	 */
	static get Input(): number;
	/**
	 * The parameter is an output parameter. - 2
	 * @type {number}
	 */
	static get Output(): number;
	/**
	 * The parameter is capable of both input and output. - 3
	 * @type {number}
	 */
	static get InputOutput(): number;
	/**
	 * The parameter represents a return value from an operation such as a stored procedure, built-in function, or user-defined function. - 6
	 * @type {number}
	 */
	static get ReturnValue(): number;
}

/** Specifies how a command string is interpreted. */
declare class CommandType {
	/**
	 * An SQL text command. (Default.) - 1
	 * @type {number}
	 */
	static get Text(): number;
	/**
	 * The name of a stored procedure. - 4
	 * @type {number}
	 */
	static get StoredProcedure(): number;
	/**
	 * The name of a table. - 512
	 * @type {number}
	 */
	static get TableDirect(): number;
}

declare module "modules" {

	/** A module that provides logging functionality to box-scripts */
	interface logger {
		/**
		 * Logs an informational message with the given arguments
		 * @param {string} message The log message
		 * @param {any[]} args The log arguments
		 * @returns {void}
		 */
		Trace(message: string, ...args: any[]): void;
		/**
		 * Logs a debug message with the given arguments
		 * @param {string} message The log message
		 * @param {any[]} args The log arguments
		 * @returns {void}
		 */
		Debug(message: string, ...args: any[]): void;
		/**
		 * Logs an informational message with the given arguments
		 * @param {string} message The log message
		 * @param {any[]} args The log arguments
		 * @returns {void}
		 */
		Info(message: string, ...args: any[]): void;
		/**
		 * Logs a warning message with the given arguments
		 * @param {string} message The log message
		 * @param {any[]} args The log arguments
		 * @returns {void}
		 */
		Warning(message: string, ...args: any[]): void;
		/**
		 * Logs an error message with the given arguments
		 * @param {string} message The log message
		 * @param {any[]} args The log arguments
		 * @returns {void}
		 */
		Error(message: string, ...args: any[]): void;
		/**
		 * Logs a critical error message with the given arguments
		 * @param {string} message The log message
		 * @param {any[]} args The log arguments
		 * @returns {void}
		 */
		Critical(message: string, ...args: any[]): void;
	}

	/** A proxy for StreamReader */
	interface StreamReaderProxy {
		/**
		 * Reads a line from the stream reader
		 * @returns {string} The read line
		 */
		ReadLine(): string;
		/**
		 * Reads a line from the stream reader asynchronously
		 * @returns {Promise<string>} The read line
		 */
		ReadLineAsync(): Promise<string>;
		/**
		 * Reads all remaining content from the stream reader
		 * @returns {string} The read content
		 */
		ReadToEnd(): string;
		/**
		 * Reads all remaining content from the stream reader asynchronously
		 * @returns {Promise<string>} The read content
		 */
		ReadToEndAsync(): Promise<string>;
		/**
		 * Closes the stream reader, releasing any resources associated with it
		 * @returns {void}
		 */
		Close(): void;
		/**
		 * Disposes of the stream reader, releasing any resources associated with it
		 * @returns {void}
		 */
		Dispose(): void;
	}

	/** A proxy for StreamWriter */
	interface StreamWriterProxy {
		/**
		 * Writes the given value to the stream writer
		 * @param {string} value The value to write
		 * @returns {void}
		 */
		Write(value: string): void;
		/**
		 * Writes the given value to the stream writer asynchronously
		 * @param {string} value The value to write
		 * @returns {Promise<void>}
		 */
		WriteAsync(value: string): Promise<void>;
		/**
		 * Writes a line with the given value to the stream writer
		 * @param {string} value The value to write
		 * @returns {void}
		 */
		WriteLine(value: string): void;
		/**
		 * Writes a line with the given value to the stream writer asynchronously
		 * @param {string} value The value to write
		 * @returns {Promise<void>}
		 */
		WriteLineAsync(value: string): Promise<void>;
		/**
		 * Flushes the stream writer, ensuring all data is written to the underlying stream
		 * @returns {void}
		 */
		Flush(): void;
		/**
		 * Flushes the stream writer asynchronously, ensuring all data is written to the underlying stream
		 * @returns {Promise<void>}
		 */
		FlushAsync(): Promise<void>;
		/**
		 * Closes the stream writer, releasing any resources associated with it
		 * @returns {void}
		 */
		Close(): void;
		/**
		 * Disposes of the stream writer, releasing any resources associated with it
		 * @returns {void}
		 */
		Dispose(): void;
	}

	/** A module that provides file system related functionalities to box-scripts */
	interface file {
		/**
		 * Copies the given file to the destination
		 * @param {string} source The source file
		 * @param {string} destination The destination path
		 * @param {boolean} [overwrite] Whether or not to overwrite the file
		 * @returns {void}
		 */
		Copy(source: string, destination: string, overwrite?: boolean): void;
		/**
		 * Moves the given file to the destination
		 * @param {string} source The source file to move
		 * @param {string} destination The destination path
		 * @param {boolean} [overwrite] Whether or not to overwrite the file
		 * @returns {void}
		 */
		Move(source: string, destination: string, overwrite?: boolean): void;
		/**
		 * Checks if the given file exists
		 * @param {string} path The file path
		 * @returns {boolean} Whether or not the file exists
		 */
		Exists(path: string): boolean;
		/**
		 * Deletes the given file
		 * @param {string} path The file path
		 * @returns {void}
		 */
		Delete(path: string): void;
		/**
		 * Appends the given text to the end of the file
		 * @param {string} path The file path
		 * @param {string} content The content
		 * @returns {void}
		 */
		Append(path: string, content: string): void;
		/**
		 * Writes the given content to the file, overwriting any existing content
		 * @param {string} path The file path
		 * @param {string} content The content
		 * @returns {void}
		 */
		Write(path: string, content: string): void;
		/**
		 * Gets the creation time of the given file
		 * @param {string} path The file path
		 * @returns {Date} The creation time of the given file
		 */
		CreateTime(path: string): Date;
		/**
		 * Gets the last time the file was accessed
		 * @param {string} path The file path
		 * @returns {Date} The last time the file was accessed
		 */
		AccessTime(path: string): Date;
		/**
		 * Gets the last time the file was written to
		 * @param {string} path The file path
		 * @returns {Date} The last time the file was written to
		 */
		WriteTime(path: string): Date;
		/**
		 * Opens a file for reading as a text stream
		 * @param {string} path The file path
		 * @returns {StreamReaderProxy} The file stream
		 */
		OpenRead(path: string): StreamReaderProxy;
		/**
		 * Opens a file for writing as a text stream
		 * @param {string} path The file path
		 * @returns {StreamWriterProxy} The file stream
		 */
		OpenWrite(path: string): StreamWriterProxy;
		/**
		 * Creates a new file at the specified path
		 * @param {string} path The file path
		 * @returns {StreamWriterProxy} The file stream
		 */
		Create(path: string): StreamWriterProxy;
		/**
		 * Ensures a file is created at the given path
		 * @param {string} path The file path
		 * @returns {void}
		 */
		Touch(path: string): void;
		/**
		 * Extracts a zip file to the specified folder
		 * @param {string} path The zip file to extract
		 * @param {string} folder The folder to extract to
		 * @param {boolean} [overwrite] Whether or not to overwrite files
		 * @returns {void}
		 */
		Unzip(path: string, folder: string, overwrite?: boolean): void;
		/**
		 * Compresses a directory into a zip file
		 * @param {string} path The folder to compress
		 * @param {string} destination The destination zip file
		 * @param {boolean} [includeBaseDir] Whether or not to include the base directory in the file
		 * @param {CompressionLevel} [level] The compression level
		 * @returns {void}
		 */
		Zip(path: string, destination: string, includeBaseDir?: boolean, level?: CompressionLevel): void;
	}

	/** A module that provides directory functionality to box-scripts */
	interface dir {
		/**
		 * Creates the given directory
		 * @param {string} path The directory to create
		 * @returns {void}
		 */
		Create(path: string): void;
		/**
		 * Whether or not the given directory exists
		 * @param {string} path The directory to check
		 * @returns {boolean} Whether or not the directory exists
		 */
		Exists(path: string): boolean;
		/**
		 * Deletes the given directory
		 * @param {string} path The directory to delete
		 * @param {boolean} [recursive] Whether or not to delete sub-directories
		 * @returns {void}
		 */
		Delete(path: string, recursive?: boolean): void;
		/**
		 * Gets the current directory
		 * @returns {string} The current directory
		 */
		Current(): string;
		/**
		 * Moves the given directory to the destination
		 * @param {string} source The source path
		 * @param {string} destination The destination path
		 * @returns {void}
		 */
		Move(source: string, destination: string): void;
		/**
		 * Gets the directories in the given path
		 * @param {string} path The path to get the files
		 * @param {string} [searchPattern] The optional search pattern
		 * @param {SearchOption} [options] The search options
		 * @returns {string[]} The file paths
		 */
		Files(path: string, searchPattern?: string, options?: SearchOption): string[];
		/**
		 * Gets the root directory or volume for the given path
		 * @param {string} path The directory path
		 * @returns {string} The root directory or volume
		 */
		Root(path: string): string;
		/**
		 * Gets the parent directory of the given path, or null if there is no parent
		 * @param {string} path The directory path
		 * @returns {string} The parent directory
		 */
		Parent(path: string): string;
	}

	/** A module that provides path related functionalities to box-scripts */
	interface path {
		/** The character used to separate path segments */
		readonly DirChar?: string;
		/** The alternate character used to separate path segments */
		readonly AltDirChar?: string;
		/** The character used to separate paths in environment variables */
		readonly Separator?: string;
		/** The character used to separate the volume from the path */
		readonly VolumeSeparator?: string;
		/**
		 * Changes the extension of the given path
		 * @param {string} path The path to change
		 * @param {string} extension The extension to use
		 * @returns {string} The changed path
		 */
		ChangeExtension(path: string, extension: string): string;
		/**
		 * Combines the given paths into a single path
		 * @param {string[]} paths The path segments
		 * @returns {string} The combined path
		 */
		Combine(...paths: string[]): string;
		/**
		 * Checks if the given path exists
		 * @param {string} path The path to check
		 * @returns {boolean} Whether or not the path exists
		 */
		Exists(path: string): boolean;
		/**
		 * Whether or not the given path ends in the directory separator
		 * @param {string} path The path to check
		 * @returns {boolean} Whether or not the given path ends in the directory separator
		 */
		EndsInDirSeparator(path: string): boolean;
		/**
		 * Gets the directory name for the given path
		 * @param {string} path The path to check
		 * @returns {string} The directory path
		 */
		Directory(path: string): string;
		/**
		 * Gets the file name for the given path
		 * @param {string} path The path
		 * @returns {string} The file name
		 */
		FileName(path: string): string;
		/**
		 * Gets the file name without the extension for the given path
		 * @param {string} path The path
		 * @returns {string} The file name without the extension
		 */
		FileNameWithoutExtension(path: string): string;
		/**
		 * Gets the extension for the given path
		 * @param {string} path The path
		 * @returns {string} The extension without the leading dot
		 */
		Extension(path: string): string;
		/**
		 * Gets the fully qualified absolute path for the given path
		 * @param {string} path The path
		 * @returns {string} The fully qualified path
		 */
		Full(path: string): string;
		/**
		 * Gets the invalid path characters
		 * @returns {string[]} The invalid path characters
		 */
		InvalidPathChars(): string[];
		/**
		 * Gets the invalid file name characters
		 * @returns {string[]} The invalid file name characters
		 */
		InvalidFileChars(): string[];
		/**
		 * Gets the path root for the given path
		 * @param {string} path The path
		 * @returns {string} The root path
		 */
		Root(path: string): string;
		/**
		 * Gets a random file name
		 * @returns {string} The random file name
		 */
		Random(): string;
		/**
		 * Gets the path relative to the given base path
		 * @param {string} relativeTo The base path
		 * @param {string} path The path to check
		 * @returns {string} The path relative to the given path
		 */
		Relative(relativeTo: string, path: string): string;
		/**
		 * Creates a temporary file and returns the path
		 * @returns {string} The temporary file path
		 */
		TempFile(): string;
		/**
		 * Gets the temporary directory
		 * @returns {string} The temporary directory
		 */
		Temp(): string;
		/**
		 * Checks if the given path is fully qualified
		 * @param {string} path The path
		 * @returns {boolean} Whether or not the given path is fully qualified
		 */
		IsQualified(path: string): boolean;
		/**
		 * Checks if the given path is rooted
		 * @param {string} path The path
		 * @returns {boolean} Whether or not the path is rooted
		 */
		IsRooted(path: string): boolean;
	}

	/** A module that provides JSON functionality to box-scripts */
	interface json {
		/**
		 * Serializes the given value to a JSON string.
		 * @param {any} value The value
		 * @returns {string} The JSON string
		 */
		Serialize(value: any): string;
		/**
		 * Deserializes the given JSON string to a JsValue object.
		 * @param {string} value The value
		 * @returns {any} The deserialized value
		 */
		Deserialize(value: string): any;
	}

	/** Represents a query parameter for an HTTP request */
	interface QueryParam {
		/** The key of the request */
		Key?: string;
		/** The value of the request */
		Value?: string;
	}

	/** Represents a range of HTTP status codes */
	interface CodeRange {
		/** The minimum value */
		Min: number;
		/** The exclusive maximum value */
		Max: number;
	}

	/** The settings for the HTTP module */
	interface HttpSettings {
		/** The method of the settings */
		Method?: string;
		/** The URL of the request */
		Url?: string;
		/** The base URL for the request */
		BaseUrl?: string;
		/** The headers for the request */
		readonly Headers?: { [key: string]: string };
		/** The query parameters for the request */
		readonly QueryParams?: QueryParam[];
		/** The status codes to expect from the request - Any code in this list will not result in an error */
		readonly ExpectStatusCodes?: CodeRange[];
		/** Whether or not to log the download progress of the HTTP request */
		LogDownload: boolean;
		/** Whether or not to log the upload progress during the HTTP request */
		LogUpload: boolean;
		/**
		 * Sets the method of the HTTP request
		 * @param {string} method The HTTP method
		 * @returns {HttpSettings} The settings for fluent method chaining
		 */
		SetMethod(method: string): HttpSettings;
		/**
		 * Sets the URL of the HTTP request
		 * @param {string} url The URL
		 * @returns {HttpSettings} The settings for fluent method chaining
		 */
		SetUrl(url: string): HttpSettings;
		/**
		 * Sets the base URL for the HTTP request
		 * @param {string} url The URL
		 * @returns {HttpSettings} The settings for fluent method chaining
		 */
		SetBaseUrl(url: string): HttpSettings;
		/**
		 * Adds a query parameter to the HTTP request
		 * @param {string} key The key of the query parameter
		 * @param {string} value The value of the query parameter
		 * @returns {HttpSettings} The settings for fluent method chaining
		 */
		AddParam(key: string, value: string): HttpSettings;
		/**
		 * Adds a header to the HTTP request
		 * @param {string} key The key of the header
		 * @param {string} value The value of the header
		 * @returns {HttpSettings} The settings for fluent method chaining
		 */
		SetHeader(key: string, value: string): HttpSettings;
		/**
		 * Sets the body to a JSON object
		 * @param {any} body The body to set
		 * @returns {HttpSettings} The settings for fluent method chaining
		 */
		JsonBody(body: any): HttpSettings;
		/**
		 * Sets the body content to a file
		 * @param {string} path The path of the file
		 * @param {string} [mpcName] The name of the multi-part form content header for the file
		 * @returns {HttpSettings} The settings for fluent method chaining
		 */
		FileBody(path: string, mpcName?: string): HttpSettings;
		/**
		 * Sets the timeout in seconds for the HTTP request
		 * @param {number} seconds The number of seconds
		 * @returns {HttpSettings} The settings for fluent method chaining
		 */
		SetTimeoutSeconds(seconds: number): HttpSettings;
		/**
		 * Clears the expected HTTP response codes
		 * @returns {HttpSettings} The settings for fluent method chaining
		 */
		ClearExpectedCodes(): HttpSettings;
		/**
		 * Adds an expected HTTP response code or range of codes - <paramref name="max" /> is exclusive
		 * @param {number} min The min code to expect
		 * @param {number} [max] The optional max code to expect
		 * @returns {HttpSettings} The settings for fluent method chaining
		 */
		ExpectCode(min: number, max?: number): HttpSettings;
		/**
		 * Sets whether or not to log the download progress during the HTTP request
		 * @param {boolean} log Whether or not to log the download progress
		 * @param {number} [seconds] How often to log the progress (in seconds)
		 * @returns {HttpSettings} The settings for fluent method chaining
		 */
		LogDownloads(log: boolean, seconds?: number): HttpSettings;
		/**
		 * Sets whether or not to log the upload progress during the HTTP request
		 * @param {boolean} log Whether or not to log the upload progress
		 * @param {number} [seconds] How often to log the progress (in seconds)
		 * @returns {HttpSettings} The settings for fluent method chaining
		 */
		LogUploads(log: boolean, seconds?: number): HttpSettings;
	}

	/** A class that holds the progress of an HTTP request. */
	interface HttpProgress {
		/** The amount of bytes uploaded */
		readonly UploadedMb: number;
		/** The percentage of the upload that has been completed */
		readonly UploadPercentage: number;
		/** The time it took to upload the data */
		readonly UploadTimeSeconds: number;
		/** Whether or not the upload has finished */
		readonly UploadFinished: boolean;
		/** The amount of bytes downloaded */
		readonly DownloadedMb: number;
		/** The percentage of the download that has been completed */
		readonly DownloadPercentage: number;
		/** The time it took to download the data */
		readonly DownloadTimeSeconds: number;
		/** Whether or not the download has finished */
		readonly DownloadFinished: boolean;
		/** The settings for the request */
		readonly Settings?: HttpSettings;
	}

	/** A wrapper for the results of an HTTP request */
	interface HttpResp {
		/** The URL of the request */
		readonly Url?: string;
		/** The response code */
		readonly Code: number;
		/** Whether or not the response status code is within the expected range. */
		readonly Valid: boolean;
		/** The response headers */
		readonly Headers?: { [key: string]: string };
		/** The progress of the request */
		readonly Progress?: HttpProgress;
		/**
		 * Throws an exception if the response status code is not in the expected range.
		 * @returns {Promise<void>}
		 */
		ThrowIfBad(): Promise<void>;
		/**
		 * Returns the response body as a JSON value
		 * @returns {Promise<any>} The response body as a JSON value
		 */
		AsJson(): Promise<any>;
		/**
		 * Returns the response body as a string
		 * @returns {Promise<string>} The response body as a string
		 */
		AsString(): Promise<string>;
		/**
		 * Writes the response body to a file
		 * @param {string} path The file path
		 * @returns {Promise<void>}
		 */
		ToFile(path: string): Promise<void>;
		/**
		 * 
		 * @returns {void}
		 */
		Dispose(): void;
	}

	/** A module that provides HTTP functionality to box-scripts */
	interface http {
		/**
		 * Creates an instance of the HTTP settings module
		 * @returns {HttpSettings} The settings that were created
		 */
		Settings(): HttpSettings;
		/**
		 * Sends an HTTP request with the given settings and returns the response message
		 * @param {HttpSettings} settings The settings for the request
		 * @returns {Promise<HttpResp>} The response
		 */
		Send(settings: HttpSettings): Promise<HttpResp>;
		/**
		 * Sends an HTTP request and returns the JSON response
		 * @param {HttpSettings} settings The settings for the request
		 * @returns {Promise<any>} The response
		 */
		SendJson(settings: HttpSettings): Promise<any>;
		/**
		 * Sends a GET request to the given URL
		 * @param {string} url The URL of the request
		 * @param {HttpSettings} [settings] The optional settings for the request
		 * @returns {Promise<HttpResp>} The response
		 */
		Get(url: string, settings?: HttpSettings): Promise<HttpResp>;
		/**
		 * Sends a GET request to the given URL and returns the JSON response
		 * @param {string} url The URL of the request
		 * @param {HttpSettings} [settings] The optional settings for the request
		 * @returns {Promise<any>} The response
		 */
		GetJson(url: string, settings?: HttpSettings): Promise<any>;
		/**
		 * Sends a GET request to the given URL
		 * @param {string} url The URL of the request
		 * @param {HttpSettings} [settings] The optional settings for the request
		 * @returns {Promise<HttpResp>} The response
		 */
		Delete(url: string, settings?: HttpSettings): Promise<HttpResp>;
		/**
		 * Sends a DELETE request to the given URL and returns the JSON response
		 * @param {string} url The URL of the request
		 * @param {HttpSettings} [settings] The optional settings for the request
		 * @returns {Promise<any>} The response
		 */
		DeleteJson(url: string, settings?: HttpSettings): Promise<any>;
		/**
		 * Sends a POST request to the given URL
		 * @param {string} url The URL of the request
		 * @param {any} body The body data of the request
		 * @param {HttpSettings} [settings] The optional settings for the request
		 * @returns {Promise<HttpResp>} The response
		 */
		Post(url: string, body: any, settings?: HttpSettings): Promise<HttpResp>;
		/**
		 * Sends a POST request to the given URL and returns the JSON response
		 * @param {string} url The URL of the request
		 * @param {any} body The body data of the request
		 * @param {HttpSettings} [settings] The optional settings for the request
		 * @returns {Promise<any>} The response
		 */
		PostJson(url: string, body: any, settings?: HttpSettings): Promise<any>;
		/**
		 * Sends a PUT request to the given URL
		 * @param {string} url The URL of the request
		 * @param {any} body The body data of the request
		 * @param {HttpSettings} [settings] The optional settings for the request
		 * @returns {Promise<HttpResp>} The response
		 */
		Put(url: string, body: any, settings?: HttpSettings): Promise<HttpResp>;
		/**
		 * Sends a PUT request to the given URL and returns the JSON response
		 * @param {string} url The URL of the request
		 * @param {any} body The body data of the request
		 * @param {HttpSettings} [settings] The optional settings for the request
		 * @returns {Promise<any>} The response
		 */
		PutJson(url: string, body: any, settings?: HttpSettings): Promise<any>;
	}

	/** A module that provides configuration functionality to box-scripts */
	interface config {
		/**
		 * Gets the value of the given configuration key
		 * @param {string} key The configuration key
		 * @returns {string} The configuration value
		 */
		Get(key: string): string;
	}

	/** Represents a database transaction. */
	interface DbTransaction {
		/**
		 * Commit the transaction
		 * @returns {void}
		 */
		Commit(): void;
		/**
		 * Rollback the transaction
		 * @returns {void}
		 */
		Rollback(): void;
		/**
		 * Dispose the transaction
		 * @returns {void}
		 */
		Dispose(): void;
	}

	/** The settings for a query */
	interface DbQuerySettings {
		/** The command timeout in seconds */
		CommandTimeoutSec: number;
		/** The command type of the transaction */
		Type: CommandType;
		/**
		 * Sets the command timeout in seconds
		 * @param {number} seconds The number of seconds to wait
		 * @returns {DbQuerySettings} The current settings for fluent chaining
		 */
		WithCommandTimeout(seconds: number): DbQuerySettings;
		/**
		 * Sets the transaction for the query
		 * @param {DbTransaction} transaction The DB transaction
		 * @returns {DbQuerySettings} The current settings for fluent chaining
		 */
		WithTransaction(transaction: DbTransaction): DbQuerySettings;
		/**
		 * Sets the command type for the query
		 * @param {CommandType} type The command type
		 * @returns {DbQuerySettings} The current settings for fluent chaining
		 */
		WithType(type: CommandType): DbQuerySettings;
		/**
		 * Adds a parameter to the query settings
		 * @param {string} name The name of the parameter
		 * @param {any} value The value of the parameter
		 * @param {DbType} [type] The type of the parameter
		 * @param {ParameterDirection} [direction] The direction of the parameter
		 * @returns {DbQuerySettings} The current settings for fluent chaining
		 */
		AddParameter(name: string, value: any, type?: DbType, direction?: ParameterDirection): DbQuerySettings;
		/**
		 * Adds multiple parameters to the query settings from an object
		 * @param {any} value The parameters
		 * @returns {DbQuerySettings} The current settings for fluent chaining
		 */
		AddParameters(value: any): DbQuerySettings;
	}

	/** A reader for multi-return results query */
	interface DbQueryReader {
		/**
		 * Reads the next result set from the query
		 * @returns {Promise<any[]>} The records from the reader
		 */
		Next(): Promise<any[]>;
		/**
		 * Reads the next result set from the query and returns the first record or null
		 * @returns {Promise<any>} The record or null
		 */
		FirstOrDefault(): Promise<any>;
		/**
		 * Reads the next result set from the query and returns the first record or null
		 * @returns {Promise<any>} The record or null
		 */
		Single(): Promise<any>;
		/**
		 * Disposes the database connection
		 * @returns {void}
		 */
		Dispose(): void;
	}

	/** The base class for every provider added to the database module. */
	interface DbConnection {
		/**
		 * Begin a database transaction
		 * @returns {DbTransaction} The database transaction
		 */
		Transaction(): DbTransaction;
		/**
		 * Creates an instance of the settings
		 * @returns {DbQuerySettings} The settings instance
		 */
		Settings(): DbQuerySettings;
		/**
		 * Execute a query that does not return any results
		 * @param {string} query The query to execute
		 * @param {DbQuerySettings} [settings] The settings for the query
		 * @returns {Promise<number>} The number of records modified
		 */
		Execute(query: string, settings?: DbQuerySettings): Promise<number>;
		/**
		 * Execute a query that returns results
		 * @param {string} query The query to execute
		 * @param {DbQuerySettings} [settings] The settings for the query
		 * @returns {Promise<any[]>} The records read from the database
		 */
		Query(query: string, settings?: DbQuerySettings): Promise<any[]>;
		/**
		 * Execute a query that returns the first result or null
		 * @param {string} query The query to execute
		 * @param {DbQuerySettings} [settings] The settings for the query
		 * @returns {Promise<any>} The records read from the database
		 */
		FirstOrDefault(query: string, settings?: DbQuerySettings): Promise<any>;
		/**
		 * Executes a query that returns a scalar result
		 * @param {string} query The query to execute
		 * @param {DbQuerySettings} [settings] The settings for the query
		 * @returns {Promise<any>} The records read from the database
		 */
		Scalar(query: string, settings?: DbQuerySettings): Promise<any>;
		/**
		 * Executes a query that returns multiple result sets
		 * @param {string} query The query to execute
		 * @param {DbQuerySettings} [settings] The settings for the query
		 * @returns {Promise<DbQueryReader>} The records read from the database
		 */
		Multiple(query: string, settings?: DbQuerySettings): Promise<DbQueryReader>;
		/**
		 * Disposes the database connection
		 * @returns {void}
		 */
		Dispose(): void;
	}

	/** The settings for connecting to the database */
	interface DbConnectionSettings {
		/** The number of times to attempt to connect to the server */
		ConnectRetries: number;
		/** The timeout between connection retries in seconds */
		readonly ConnectRetryTimeoutSec: number;
		/** The connection string to use */
		ConnectionString?: string;
		/**
		 * Sets the connection string to use for the database connection
		 * @param {string} conString The connection string
		 * @returns {DbConnectionSettings} The current settings for fluent chaining
		 */
		WithConnection(conString: string): DbConnectionSettings;
		/**
		 * Sets the number of times to attempt to connect to the server
		 * @param {number} count The number of retries
		 * @returns {DbConnectionSettings} The current settings for fluent chaining
		 */
		WithConnectRetryCount(count: number): DbConnectionSettings;
		/**
		 * Sets the timeout between connection retries in seconds
		 * @param {number} seconds The number of seconds to wait
		 * @returns {DbConnectionSettings} The current settings for fluent chaining
		 */
		WithConnectRetryTimeout(seconds: number): DbConnectionSettings;
		/**
		 * Connects to the database using the provided connection string and settings.
		 * @returns {Promise<DbConnection>} The open connection
		 */
		Connect(): Promise<DbConnection>;
	}

	/** The Connection Provider for connecting to a MySQL database */
	interface MySqlProvider {
		/** The number of times to attempt to connect to the server */
		ConnectRetries: number;
		/** The timeout between connection retries in seconds */
		readonly ConnectRetryTimeoutSec: number;
		/** The connection string to use */
		ConnectionString?: string;
		/**
		 * Sets the connection string to use for the database connection
		 * @param {string} conString The connection string
		 * @returns {DbConnectionSettings} The current settings for fluent chaining
		 */
		WithConnection(conString: string): DbConnectionSettings;
		/**
		 * Sets the number of times to attempt to connect to the server
		 * @param {number} count The number of retries
		 * @returns {DbConnectionSettings} The current settings for fluent chaining
		 */
		WithConnectRetryCount(count: number): DbConnectionSettings;
		/**
		 * Sets the timeout between connection retries in seconds
		 * @param {number} seconds The number of seconds to wait
		 * @returns {DbConnectionSettings} The current settings for fluent chaining
		 */
		WithConnectRetryTimeout(seconds: number): DbConnectionSettings;
		/**
		 * Connects to the database using the provided connection string and settings.
		 * @returns {Promise<DbConnection>} The open connection
		 */
		Connect(): Promise<DbConnection>;
	}

	/** The Connection Provider for connecting to a PostgreSQL database */
	interface NpgsqlProvider {
		/** The number of times to attempt to connect to the server */
		ConnectRetries: number;
		/** The timeout between connection retries in seconds */
		readonly ConnectRetryTimeoutSec: number;
		/** The connection string to use */
		ConnectionString?: string;
		/**
		 * Sets the connection string to use for the database connection
		 * @param {string} conString The connection string
		 * @returns {DbConnectionSettings} The current settings for fluent chaining
		 */
		WithConnection(conString: string): DbConnectionSettings;
		/**
		 * Sets the number of times to attempt to connect to the server
		 * @param {number} count The number of retries
		 * @returns {DbConnectionSettings} The current settings for fluent chaining
		 */
		WithConnectRetryCount(count: number): DbConnectionSettings;
		/**
		 * Sets the timeout between connection retries in seconds
		 * @param {number} seconds The number of seconds to wait
		 * @returns {DbConnectionSettings} The current settings for fluent chaining
		 */
		WithConnectRetryTimeout(seconds: number): DbConnectionSettings;
		/**
		 * Connects to the database using the provided connection string and settings.
		 * @returns {Promise<DbConnection>} The open connection
		 */
		Connect(): Promise<DbConnection>;
	}

	/** The Connection Provider for connecting to a Microsoft SQL Server database */
	interface MsSqlProvider {
		/** The number of times to attempt to connect to the server */
		ConnectRetries: number;
		/** The timeout between connection retries in seconds */
		readonly ConnectRetryTimeoutSec: number;
		/** The connection string to use */
		ConnectionString?: string;
		/**
		 * Sets the connection string to use for the database connection
		 * @param {string} conString The connection string
		 * @returns {DbConnectionSettings} The current settings for fluent chaining
		 */
		WithConnection(conString: string): DbConnectionSettings;
		/**
		 * Sets the number of times to attempt to connect to the server
		 * @param {number} count The number of retries
		 * @returns {DbConnectionSettings} The current settings for fluent chaining
		 */
		WithConnectRetryCount(count: number): DbConnectionSettings;
		/**
		 * Sets the timeout between connection retries in seconds
		 * @param {number} seconds The number of seconds to wait
		 * @returns {DbConnectionSettings} The current settings for fluent chaining
		 */
		WithConnectRetryTimeout(seconds: number): DbConnectionSettings;
		/**
		 * Connects to the database using the provided connection string and settings.
		 * @returns {Promise<DbConnection>} The open connection
		 */
		Connect(): Promise<DbConnection>;
	}

	/** The Connection Provider for connecting to a SQLite database */
	interface SqliteProvider {
		/** The number of times to attempt to connect to the server */
		ConnectRetries: number;
		/** The timeout between connection retries in seconds */
		readonly ConnectRetryTimeoutSec: number;
		/** The connection string to use */
		ConnectionString?: string;
		/**
		 * Sets the connection string to use for the database connection
		 * @param {string} conString The connection string
		 * @returns {DbConnectionSettings} The current settings for fluent chaining
		 */
		WithConnection(conString: string): DbConnectionSettings;
		/**
		 * Sets the number of times to attempt to connect to the server
		 * @param {number} count The number of retries
		 * @returns {DbConnectionSettings} The current settings for fluent chaining
		 */
		WithConnectRetryCount(count: number): DbConnectionSettings;
		/**
		 * Sets the timeout between connection retries in seconds
		 * @param {number} seconds The number of seconds to wait
		 * @returns {DbConnectionSettings} The current settings for fluent chaining
		 */
		WithConnectRetryTimeout(seconds: number): DbConnectionSettings;
		/**
		 * Connects to the database using the provided connection string and settings.
		 * @returns {Promise<DbConnection>} The open connection
		 */
		Connect(): Promise<DbConnection>;
	}

	/** A module that provides database functionality to box-scripts */
	interface db {
		/**
		 * Creates an instance of the MySQL connection settings module
		 * @returns {MySqlProvider} The connection instance that were created
		 */
		MySql(): MySqlProvider;
		/**
		 * Creates an instance of the PostgreSQL connection settings module
		 * @returns {NpgsqlProvider} The connection instance that were created
		 */
		Postgres(): NpgsqlProvider;
		/**
		 * Creates an instance of the SQL Server connection settings module
		 * @returns {MsSqlProvider} The connection instance that were created
		 */
		SqlServer(): MsSqlProvider;
		/**
		 * Creates an instance of the SQLite connection settings module
		 * @returns {SqliteProvider} The connection instance that were created
		 */
		SQLite(): SqliteProvider;
	}

	export var logger: logger;
	export var file: file;
	export var dir: dir;
	export var path: path;
	export var json: json;
	export var http: http;
	export var config: config;
	export var db: db;
}
