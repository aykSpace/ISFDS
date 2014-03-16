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

INSERT INTO Spacecraft.MassInertialCharacteristic (ID_Aero, Mass, MassOfCommonBay, MassOfDeorbitSpCr, Sbal, XT, YT, ZT, Liter, DateOfID, Comment)
VALUES (0, 40500, 0, 0, 0.009256, 0, 0, 0, NULL, GETDATE(), N'МКС тест1' )

SELECT * FROM Spacecraft.MassInertialCharacteristic

INSERT INTO Spacecraft.SpacecraftInitialData (SpacecraftNumber, SpacecraftName, SpacecraftType, OrbitType, Launcher, DateOfLaunch,
											  MassInerCharacteristicId, EngineID, Comments)
VALUES (1, N'МКС', N'Пилотируемая станция', N'Низкоорбитальная', N'Протон', '1998-20-11 09:40:00', 2, 0, N'ФГБ "Заря"')



INSERT INTO dbo.NU (N_NU, SpacecraftInitialData_ID, Modification, X, Y,Z, VX, VY, VZ, t, Vitok, Sbal, DateNU, Comment)
VALUES (110,1,1, 341.554678, -44444.99966, 4.9965456, 0.999523456, -1.4995678329,  0.00054, 70500, 1816, 0.04158185, '2011/10/05', N'Тестовые НУ') 
SELECT * FROM NU

INSERT INTO dbo.NU (N_NU, SpacecraftInitialData_ID, Modification, X, Y,Z, VX, VY, VZ, t, Vitok, Sbal, DateNU, Comment)
VALUES (220,1,5, -714.159212, 6753.86879, 248.3338637, -4.21146536, -0.67453418, 6.00246598, 31620, 3287, 0.0487, '2014/18/02 08:47:00', N'НУ МКС') 

INSERT INTO dbo.NU (N_NU, SpacecraftInitialData_ID, Modification, X, Y,Z, VX, VY, VZ, t, Vitok, Sbal, DateNU, Comment)
VALUES (220,2,5, -714.159212, 6753.86879, 248.3338637, -4.21146536, -0.67453418, 6.00246598, 31620, 3287, 0.0487, '2014/18/02 08:47:00', N'Пристыкованного корабля') 


INSERT INTO [Spacecraft].[SpaceсraftCommonData] ([SpacecraftInitDataId],[MIC_Id],[EngineId])
VALUES (1,1,0)

INSERT INTO [Spacecraft].[SpaceсraftCommonData] ([SpacecraftInitDataId],[MIC_Id],[EngineId])
VALUES (1,3,0)
SELECT * FROM [Spacecraft].[SpaceсraftCommonData]


INSERT INTO Spacecraft.Engine (NameEngine, Trust, SpecificImpulse, FuelAmount, MaxTimeOfWorking, TypeOfEngine, Comment)
VALUES (N'Маршевый двигатель', 0.0014, 1000, null, 6000, N'ЭрДУ', N'Электрореактивная двигательная установка')

SELECT * FROM Spacecraft.Engine

INSERT INTO Spacecraft.SpacecraftInitialData (SpacecraftNumber, SpacecraftName, SpacecraftType, OrbitType, Launcher, ReboostBlockType, DateOfLaunch,
											  MassInerCharacteristicId, EngineID, Comments)
VALUES (2, N'Канопус-В', N'ДЗЗ', N'Солнечносинхронная', N'Союз', N'Фрегат', '2012-22-07 09:41:38', 3, 1, N'Спутник для съемок Земли с солнечносинхронной орбиты')

SELECT * FROM Spacecraft.SpacecraftInitialData

SELECT * FROM Spacecraft.MassInertialCharacteristic
UPDATE Spacecraft.SpacecraftInitialData SET MassInerCharacteristicId = 2 WHERE SpacecraftInitDataId = 3 

SELECT * FROM NU