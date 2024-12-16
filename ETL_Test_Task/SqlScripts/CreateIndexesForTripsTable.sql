USE TripsDB;

-- Rebuild or create index for `PULocationID`
IF EXISTS (
    SELECT 1 
    FROM sys.indexes 
    WHERE name = 'idx_pulocationid' AND object_id = OBJECT_ID('Trips')
)
BEGIN
    ALTER INDEX idx_pulocationid ON Trips REBUILD;
END
ELSE
BEGIN
    CREATE INDEX idx_pulocationid ON Trips (PULocationID);  
END;

-- Rebuild or create index for `TripDistance`
IF EXISTS (
    SELECT 1 
    FROM sys.indexes 
    WHERE name = 'idx_tripdistance' AND object_id = OBJECT_ID('Trips')
)
BEGIN
    ALTER INDEX idx_tripdistance ON Trips REBUILD;
END
ELSE
BEGIN
    CREATE INDEX idx_tripdistance ON Trips (TripDistance DESC);  
END;

-- Rebuild or create index for `TravelDurationInSeconds`
IF EXISTS (
    SELECT 1 
    FROM sys.indexes 
    WHERE name = 'idx_travel_duration' AND object_id = OBJECT_ID('Trips')
)
BEGIN
    ALTER INDEX idx_travel_duration ON Trips REBUILD;
END
ELSE
BEGIN
    CREATE INDEX idx_travel_duration ON Trips (TravelDurationInSeconds DESC);  
END;