The SQL scripts for creating the database and table are located in the project files at the path `ETL_Test_Task/SqlScripts`.

Number of rows in my table after running the program - 29840.

Comments on any assumptions made:
  - if any of the fields needed for the table is NULL (except TpepPickupDatetime and TpepDropoffDatetime - I made an assumption that these date columns are always available) - I'm skipping that record;
  - the app should be run via the command line with 2 arguments (they are positional): 1st - for the input csv data file, 2nd - for the database connection string (with master database specified for the database, for example `"Server=.\\SQLExpress;Database=master;Trusted_Connection=True;TrustServerCertificate=True;"`);
  - I created computed TravelDurationInSeconds column in the table to improve performance for the task query "Find the top 100 longest fares in terms of time spent traveling".

Regarding "9. Assume your program will be used on much larger data files. Describe in a few sentences what you would change if you knew it would be used for a 10GB CSV input file.".
To handle a 10GB CSV input file efficiently, I would make the following changes to the program:

  - instead of loading the entire CSV file into memory at once, I would use a streaming approach to read and process the file in chunks;

  - I would split the data into smaller batches (for example, 10 000 - 50 000 rows at a time) and perform SqlBulkCopy in batches;

  - I would process chunks of the CSV file in parallel (using Task or Parallel.ForEach);

  - I would disable non-clustered indexes temporarily during the bulk insert to speed up the process and rebuild them afterwards.
