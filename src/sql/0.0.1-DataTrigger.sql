CREATE TABLE DeviceStateHistory(
	Id UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
	DeviceId NVARCHAR(MAX) NOT NULL,
	Obstructed BIT NOT NULL DEFAULT 0,
	BatteryLevel INT NOT NULL DEFAULT 0,
	EventDate DATETIME NOT NULL DEFAULT GETUTCDATE()
);

 CREATE TABLE DeviceRegistration(
	Id UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
	DeviceId NVARCHAR(MAX) NOT NULL,
	EntityId UNIQUEIDENTIFIER NULL
  );

ALTER TABLE DeviceRegistration
ADD EntityId UNIQUEIDENTIFIER NULL

CREATE TRIGGER T_UpdateDeviceState ON DeviceStateHistory FOR INSERT AS 
BEGIN
    SET NOCOUNT ON;
	DECLARE @deviceCount INT;
	DECLARE @deviceId NVARCHAR(max);
	DECLARE @batteryLevel INT;
	DECLARE @obstructed BIT;

	SET @deviceId = (SELECT DeviceId FROM Inserted);
	SET @batteryLevel = (SELECT BatteryLevel FROM Inserted);
	SET @obstructed = (SELECT Obstructed FROM Inserted);

	SET @deviceCount = (SELECT COUNT(*) FROM DeviceState WHERE DeviceId = @deviceId);


	IF @deviceCount = 0
	BEGIN
		INSERT INTO DeviceRegistration(DeviceId) VALUES(@deviceId);
		IF @deviceId IS NOT NULL
		BEGIN
			INSERT INTO DeviceState(DeviceId, BatteryLevel, Obstructed) VALUES(@deviceId, @batteryLevel, @obstructed);
		END
	END
	ELSE
	BEGIN
		UPDATE DeviceState SET BatteryLevel = @batteryLevel, Obstructed = @obstructed, UpdateAt = GETUTCDATE() WHERE DeviceId = @deviceId;
	END
END