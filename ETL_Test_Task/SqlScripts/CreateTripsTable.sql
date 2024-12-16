USE TripsDB;

-- Check if the table exists, and create it if it doesn't
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Trips')
BEGIN
    -- Create table for trip data
    CREATE TABLE Trips (
        Id INT IDENTITY PRIMARY KEY,
        TpepPickupDatetime DATETIME2(3) NOT NULL,
        TpepDropoffDatetime DATETIME2(3) NOT NULL,
        PassengerCount INT NULL,
        TripDistance DECIMAL(10, 2) NOT NULL,
        StoreAndFwdFlag NVARCHAR(3) NOT NULL,
        PULocationID INT NOT NULL,
        DOLocationID INT NOT NULL,
        FareAmount DECIMAL(19, 2) NOT NULL,
        TipAmount DECIMAL(19, 2) NOT NULL,
        TravelDurationInSeconds AS DATEDIFF(SECOND, TpepPickupDatetime, TpepDropoffDatetime) PERSISTED
    );
END
