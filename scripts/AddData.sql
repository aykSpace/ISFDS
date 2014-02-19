USE ISFDS
GO
INSERT INTO Spacecraft.Engine (ID_Engine, NameEngine, Trust, SpecificImpulse, FuelAmount, MaxTimeOfWorking, TypeOfEngine, Comment)
VALUES (0, N'Dummy engine', 0, 0, 0, 0, N'dummy', N'This is dummy engine for passive spacecraft' )

SET IDENTITY_INSERT [ISFDS].[Spacecraft].[Engine] OFF

SELECT * FROM Spacecraft.Engine

SELECT * FROM Spacecraft.MassInertialCharacteristic

INSERT INTO Spacecraft.MassInertialCharacteristic (ID_Aero, Mass, MassOfCommonBay, MassOfDeorbitSpCr, Sbal, XT, YT, ZT, Liter, DateOfID, Comment)
VALUES (0, 6707.5, 1199.8, 2861, 0.008234, 0.385082, -0.038695, 0, N'Д', '2013-11-13', N'Корабль после стыковки' )

INSERT INTO Spacecraft.SpacecraftInitialData (SpacecraftNumber, SpacecraftName, SpacecraftType, OrbitType, Launcher, DateOfLaunch,
											  MassInerCharacteristicId, EngineID, Comments)
VALUES (711, N'Союз ТМА-11М', N'ТПК', N'Низкоорбитальная', N'Союз-ФГ', '2013-13-11 08:14:15', 1, 0, N'Пристыкованный к МКС ТПК')

SELECT * FROM Spacecraft.SpacecraftInitialData



DBCC CHECKIDENT('Spacecraft.SpacecraftInitialData',RESEED,0)
DELETE Spacecraft.SpacecraftInitialData


ALTER TABLE [dbo].[NU] NOCHECK CONSTRAINT [FK_NU_SpacecraftInitialData]

INSERT INTO Spacecraft.MassInertialCharacteristic (ID_Aero, Mass, MassOfCommonBay, MassOfDeorbitSpCr, Sbal, XT, YT, ZT, Liter, DateOfID, Comment)
VALUES (0, 40000, 0, 0, 0.008234, 0, 0, 0, NULL, GETDATE(), N'МКС тест' )

INSERT INTO Spacecraft.SpacecraftInitialData (SpacecraftNumber, SpacecraftName, SpacecraftType, OrbitType, Launcher, DateOfLaunch,
											  MassInerCharacteristicId, EngineID, Comments)
VALUES (1, N'МКС', N'Пилотируемая станция', N'Низкоорбитальная', N'Протон', '1998-20-11 09:40:00', 2, 0, N'ФГБ "Заря"')