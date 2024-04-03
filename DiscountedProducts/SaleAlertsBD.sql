CREATE DATABASE SaleAlerts;
GO
USE SaleAlerts;
GO
CREATE TABLE Countries (
    CountryID INT IDENTITY(1,1) PRIMARY KEY,
    CountryName NVARCHAR(100)
);
GO
INSERT INTO Countries (CountryName)
VALUES
('Украина'),
('Соединенные Штаты'),
('Канада'),
('Франция'),
('Германия'),
('Япония'),
('Австралия'),
('Китай'),
('Индия'),
('Бразилия');

CREATE TABLE Cities (
    CityID INT IDENTITY(1,1) PRIMARY KEY,
    CityName NVARCHAR(100),
    CountryID INT,
    FOREIGN KEY (CountryID) REFERENCES Countries(CountryID) ON DELETE CASCADE
);
GO
INSERT INTO Cities (CityName, CountryID)
VALUES
('Киев', 1),
('Львов', 1),
('Вашингтон', 2),
('Нью-Йорк', 2),
('Оттава', 3),
('Торонто', 3), 
('Париж', 4),
('Марсель', 4), 
('Берлин', 5),
('Мюнхен', 5), 
('Токио', 6),
('Осака', 6), 
('Сидней', 7),
('Мельбурн', 7),
('Пекин', 8),
('Шанхай', 8), 
('Дели', 9),
('Мумбаи', 9), 
('Бразилиа', 10),
('Сан-Паулу', 10);
GO

CREATE TABLE UserProfiles (
    CustomerID INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(255),
    BirthDate DATE,
    Gender CHAR(1),
    Email NVARCHAR(255),
    CityID INT,
    FOREIGN KEY (CityID) REFERENCES Cities(CityID) ON DELETE CASCADE
);
GO

INSERT INTO UserProfiles (FullName, BirthDate, Gender, Email, CityID)
VALUES
('Алексей Смирнов', '1985-02-15', 'M', 'aleksey.smirnov@email.com', 1),
('Мария Иванова', '1992-07-24', 'F', 'maria.ivanova@email.com', 2),
('Игорь Николаев', '1988-03-30', 'M', 'igor.nikolaev@email.com', 3),
('Ольга Петрова', '1990-11-12', 'F', 'olga.petrova@email.com', 4),
('Дмитрий Козлов', '1975-05-29', 'M', 'dmitry.kozlov@email.com', 5),
('Елена Соколова', '1982-08-21', 'F', 'elena.sokolova@email.com', 6),
('Сергей Михайлов', '1995-01-14', 'M', 'sergey.mikhailov@email.com', 7),
('Анна Кузнецова', '1987-04-17', 'F', 'anna.kuznetsova@email.com', 8),
('Николай Васильев', '1979-12-02', 'M', 'nikolay.vasilyev@email.com', 9),
( 'Татьяна Морозова', '1993-09-10', 'F', 'tatiana.morozova@email.com', 10),
( 'Владимир Ершов', '1984-07-23', 'M', 'vladimir.yershov@email.com', 11),
( 'Светлана Никитина', '1978-02-14', 'F', 'svetlana.nikitina@email.com', 12),
( 'Антон Захаров', '1991-03-28', 'M', 'anton.zakharov@email.com', 13),
( 'Юлия Семенова', '1994-10-19', 'F', 'yulia.semenova@email.com', 14),
( 'Павел Виноградов', '1986-06-07', 'M', 'pavel.vinogradov@email.com', 15),
( 'Ирина Михеева', '1981-12-30', 'F', 'irina.miheeva@email.com', 16),
( 'Егор Андреев', '1997-08-25', 'M', 'egor.andreev@email.com', 17),
( 'Наталья Рыбакова', '1983-01-16', 'F', 'natalya.rybakova@email.com', 18),
( 'Артем Козырев', '1976-04-22', 'M', 'artem.kozyrev@email.com', 19),
( 'Виктория Гусева', '1995-07-05', 'F', 'viktoria.guseva@email.com', 20);

CREATE TABLE ProductInterests (
    InterestID INT IDENTITY(1,1) PRIMARY KEY,
    InterestName NVARCHAR(100)
);
GO
INSERT INTO ProductInterests (InterestName)
VALUES
('Техника'),
('Книги'),
('Спортивные товары'),
('Электроника'),
('Мебель'),
('Игрушки'),
('Одежда'),
('Косметика'),
('Путешествия'),
('Автоаксессуары');


CREATE TABLE UserProfileInterests (
    CustomerID INT,
    InterestID INT,
    PRIMARY KEY (CustomerID, InterestID),
    FOREIGN KEY (CustomerID) REFERENCES UserProfiles(CustomerID) ON DELETE CASCADE,
    FOREIGN KEY (InterestID) REFERENCES ProductInterests(InterestID) ON DELETE CASCADE
);
GO
INSERT INTO UserProfileInterests (CustomerID, InterestID)
VALUES
(1, 1),
(1, 2),
(2, 3),
(3, 4),
(4, 5),
(5, 6),
(6, 1),
(7, 2),
(8, 3),
(9, 4),
(10, 5),
(11, 6),
(12, 7),
(13, 8),
(14, 9),
(15, 10),
(16, 7),
(17, 8),
(18, 9),
(19, 10),
(20, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(7, 7),
(8, 8),
(9, 9),
(10, 10),
(11, 1),
(12, 2),
(13, 3),
(14, 4),
(15, 5),
(16, 6),
(17, 7),
(18, 8),
(19, 9),
(20, 10);

CREATE TABLE SpecialOffers (
    PromotionID INT IDENTITY(1,1) PRIMARY KEY,
    InterestID INT,
    CountryID INT,
    StartDate DATE,
    EndDate DATE,
    PromotionDetails NVARCHAR(500),
    FOREIGN KEY (InterestID) REFERENCES ProductInterests(InterestID) ON DELETE CASCADE,
    FOREIGN KEY (CountryID) REFERENCES Countries(CountryID) ON DELETE CASCADE
);
GO
INSERT INTO SpecialOffers (InterestID, CountryID, StartDate, EndDate, PromotionDetails)
VALUES
(1, 1, '2024-04-01', '2024-04-30', 'Скидки на технику до 20% в апреле!'),
(2, 2, '2024-05-01', '2024-05-15', 'Распродажа книг - купи две, третья в подарок!'),
(3, 3, '2024-06-01', '2024-06-30', 'Скидки на спортивные товары до 25%'),
(4, 4, '2024-07-01', '2024-07-20', 'Летняя распродажа электроники!'),
(5, 5, '2024-08-01', '2024-08-31', 'Мебель со скидками до 30%!'),
(6, 6, '2024-09-01', '2024-09-15', 'Игрушки для детей со скидкой 20%!'),
(7, 7, '2024-10-01', '2024-10-31', 'Осенняя коллекция одежды - скидки до 25%!'),
(8, 8, '2024-11-01', '2024-11-30', 'Косметика и парфюмерия со скидками до 20%!'),
(9, 9, '2024-12-01', '2024-12-31', 'Специальные предложения на путешествия зимой!'),
(10, 10, '2024-01-01', '2024-01-20', 'Автоаксессуары со скидкой 15% в новом году!');

INSERT INTO SpecialOffers (InterestID, CountryID, StartDate, EndDate, PromotionDetails)
VALUES
(10, 1, '2024-02-01', '2024-02-28', 'Зимняя распродажа автоаксессуаров со скидками до 30%!'),
(9, 2, '2024-03-01', '2024-03-20', 'Весенние скидки на путешествия - до 50%!'),
(8, 3, '2024-04-10', '2024-04-30', 'Скидки на косметику для весеннего обновления!'),
(7, 4, '2024-05-01', '2024-05-15', 'Новая коллекция одежды - специальные цены!'),
(6, 5, '2024-06-01', '2024-06-20', 'Летние игрушки для настоящих приключений со скидкой!'),
(5, 6, '2024-07-15', '2024-08-15', 'Мебель для дома и сада - идеальные предложения для лета!'),
(4, 7, '2024-08-01', '2024-08-31', 'Эксклюзивные скидки на электронику до 40%!'),
(3, 8, '2024-09-05', '2024-09-25', 'Осенняя распродажа спортивных товаров!'),
(2, 9, '2024-10-01', '2024-10-31', 'Книжная ярмарка - скидки и подарки!'),
(1, 10, '2024-11-10', '2024-11-30', 'Черная пятница: техника по самым низким ценам года!');
