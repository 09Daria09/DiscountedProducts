CREATE DATABASE SaleAlerts;
GO

USE SaleAlerts;
GO

CREATE TABLE Countries (
    CountryID INT PRIMARY KEY,
    CountryName NVARCHAR(100)
);
GO
INSERT INTO Countries (CountryID, CountryName)
VALUES
(1, 'Украина'),
(2, 'Соединенные Штаты'),
(3, 'Канада'),
(4, 'Франция'),
(5, 'Германия'),
(6, 'Япония'),
(7, 'Австралия'),
(8, 'Китай'),
(9, 'Индия'),
(10, 'Бразилия');

CREATE TABLE Cities (
    CityID INT PRIMARY KEY,
    CityName NVARCHAR(100),
    CountryID INT,
    FOREIGN KEY (CountryID) REFERENCES Countries(CountryID)
);
GO
INSERT INTO Cities (CityID, CityName, CountryID)
VALUES
(1, 'Киев', 1),
(2, 'Львов', 1),
(3, 'Вашингтон', 2),
(4, 'Нью-Йорк', 2),
(5, 'Оттава', 3),
(6, 'Торонто', 3), 
(7, 'Париж', 4),
(8, 'Марсель', 4), 
(9, 'Берлин', 5),
(10, 'Мюнхен', 5), 
(11, 'Токио', 6),
(12, 'Осака', 6), 
(13, 'Сидней', 7),
(14, 'Мельбурн', 7),
(15, 'Пекин', 8),
(16, 'Шанхай', 8), 
(17, 'Дели', 9),
(18, 'Мумбаи', 9), 
(19, 'Бразилиа', 10),
(20, 'Сан-Паулу', 10);

CREATE TABLE UserProfiles (
    CustomerID INT PRIMARY KEY,
    FullName NVARCHAR(255),
    BirthDate DATE,
    Gender CHAR(1),
    Email NVARCHAR(255),
    CityID INT,
    FOREIGN KEY (CityID) REFERENCES Cities(CityID)
);
GO
INSERT INTO UserProfiles (CustomerID, FullName, BirthDate, Gender, Email, CityID)
VALUES
(1, 'Алексей Смирнов', '1985-02-15', 'M', 'aleksey.smirnov@email.com', 1),
(2, 'Мария Иванова', '1992-07-24', 'F', 'maria.ivanova@email.com', 2),
(3, 'Игорь Николаев', '1988-03-30', 'M', 'igor.nikolaev@email.com', 3),
(4, 'Ольга Петрова', '1990-11-12', 'F', 'olga.petrova@email.com', 4),
(5, 'Дмитрий Козлов', '1975-05-29', 'M', 'dmitry.kozlov@email.com', 5),
(6, 'Елена Соколова', '1982-08-21', 'F', 'elena.sokolova@email.com', 6),
(7, 'Сергей Михайлов', '1995-01-14', 'M', 'sergey.mikhailov@email.com', 7),
(8, 'Анна Кузнецова', '1987-04-17', 'F', 'anna.kuznetsova@email.com', 8),
(9, 'Николай Васильев', '1979-12-02', 'M', 'nikolay.vasilyev@email.com', 9),
(10, 'Татьяна Морозова', '1993-09-10', 'F', 'tatiana.morozova@email.com', 10),
(11, 'Владимир Ершов', '1984-07-23', 'M', 'vladimir.yershov@email.com', 11),
(12, 'Светлана Никитина', '1978-02-14', 'F', 'svetlana.nikitina@email.com', 12),
(13, 'Антон Захаров', '1991-03-28', 'M', 'anton.zakharov@email.com', 13),
(14, 'Юлия Семенова', '1994-10-19', 'F', 'yulia.semenova@email.com', 14),
(15, 'Павел Виноградов', '1986-06-07', 'M', 'pavel.vinogradov@email.com', 15),
(16, 'Ирина Михеева', '1981-12-30', 'F', 'irina.miheeva@email.com', 16),
(17, 'Егор Андреев', '1997-08-25', 'M', 'egor.andreev@email.com', 17),
(18, 'Наталья Рыбакова', '1983-01-16', 'F', 'natalya.rybakova@email.com', 18),
(19, 'Артем Козырев', '1976-04-22', 'M', 'artem.kozyrev@email.com', 19),
(20, 'Виктория Гусева', '1995-07-05', 'F', 'viktoria.guseva@email.com', 20);

CREATE TABLE ProductInterests (
    InterestID INT PRIMARY KEY,
    InterestName NVARCHAR(100)
);
GO
INSERT INTO ProductInterests (InterestID, InterestName)
VALUES
(1, 'Техника'),
(2, 'Книги'),
(3, 'Спортивные товары'),
(4, 'Электроника'),
(5, 'Мебель'),
(6, 'Игрушки'),
(7, 'Одежда'),
(8, 'Косметика'),
(9, 'Путешествия'),
(10, 'Автоаксессуары');


CREATE TABLE UserProfileInterests (
    CustomerID INT,
    InterestID INT,
    PRIMARY KEY (CustomerID, InterestID),
    FOREIGN KEY (CustomerID) REFERENCES UserProfiles(CustomerID),
    FOREIGN KEY (InterestID) REFERENCES ProductInterests(InterestID)
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
    PromotionID INT PRIMARY KEY,
    InterestID INT,
    CountryID INT,
    StartDate DATE,
    EndDate DATE,
    PromotionDetails NVARCHAR(500),
    FOREIGN KEY (InterestID) REFERENCES ProductInterests(InterestID),
    FOREIGN KEY (CountryID) REFERENCES Countries(CountryID)
);
GO
INSERT INTO SpecialOffers (PromotionID, InterestID, CountryID, StartDate, EndDate, PromotionDetails)
VALUES
(1, 1, 1, '2024-04-01', '2024-04-30', 'Скидки на технику до 20% в апреле!'),
(2, 2, 2, '2024-05-01', '2024-05-15', 'Распродажа книг - купи две, третья в подарок!'),
(3, 3, 3, '2024-06-01', '2024-06-30', 'Скидки на спортивные товары до 25%'),
(4, 4, 4, '2024-07-01', '2024-07-20', 'Летняя распродажа электроники!'),
(5, 5, 5, '2024-08-01', '2024-08-31', 'Мебель со скидками до 30%!'),
(6, 6, 6, '2024-09-01', '2024-09-15', 'Игрушки для детей со скидкой 20%!'),
(7, 7, 7, '2024-10-01', '2024-10-31', 'Осенняя коллекция одежды - скидки до 25%!'),
(8, 8, 8, '2024-11-01', '2024-11-30', 'Косметика и парфюмерия со скидками до 20%!'),
(9, 9, 9, '2024-12-01', '2024-12-31', 'Специальные предложения на путешествия зимой!'),
(10, 10, 10, '2024-01-01', '2024-01-20', 'Автоаксессуары со скидкой 15% в новом году!');

INSERT INTO SpecialOffers (PromotionID, InterestID, CountryID, StartDate, EndDate, PromotionDetails)
VALUES
(11, 10, 1, '2024-02-01', '2024-02-28', 'Зимняя распродажа автоаксессуаров со скидками до 30%!'),
(12, 9, 2, '2024-03-01', '2024-03-20', 'Весенние скидки на путешествия - до 50%!'),
(13, 8, 3, '2024-04-10', '2024-04-30', 'Скидки на косметику для весеннего обновления!'),
(14, 7, 4, '2024-05-01', '2024-05-15', 'Новая коллекция одежды - специальные цены!'),
(15, 6, 5, '2024-06-01', '2024-06-20', 'Летние игрушки для настоящих приключений со скидкой!'),
(16, 5, 6, '2024-07-15', '2024-08-15', 'Мебель для дома и сада - идеальные предложения для лета!'),
(17, 4, 7, '2024-08-01', '2024-08-31', 'Эксклюзивные скидки на электронику до 40%!'),
(18, 3, 8, '2024-09-05', '2024-09-25', 'Осенняя распродажа спортивных товаров!'),
(19, 2, 9, '2024-10-01', '2024-10-31', 'Книжная ярмарка - скидки и подарки!'),
(20, 1, 10, '2024-11-10', '2024-11-30', 'Черная пятница: техника по самым низким ценам года!');

CREATE TABLE InterestSpecificOffers (
    InterestID INT,
    PromotionID INT,
    PRIMARY KEY (InterestID, PromotionID),
    FOREIGN KEY (InterestID) REFERENCES ProductInterests(InterestID),
    FOREIGN KEY (PromotionID) REFERENCES SpecialOffers(PromotionID)
);
GO
INSERT INTO InterestSpecificOffers (InterestID, PromotionID)
VALUES
(1, 1), (1, 20),
(2, 2), (2, 19),
(3, 3), (3, 18),
(4, 4), (4, 17),
(5, 5), (5, 16),
(6, 6), (6, 15),
(7, 7), (7, 14),
(8, 8), (8, 13),
(9, 9), (9, 12),
(10, 10), (10, 11),
(1, 10), (2, 9), (3, 8), (4, 7), (5, 6),
(6, 5), (7, 4), (8, 3), (9, 2), (10, 1);
