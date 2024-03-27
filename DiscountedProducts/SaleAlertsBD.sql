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
(1, '�������'),
(2, '����������� �����'),
(3, '������'),
(4, '�������'),
(5, '��������'),
(6, '������'),
(7, '���������'),
(8, '�����'),
(9, '�����'),
(10, '��������');

CREATE TABLE Cities (
    CityID INT PRIMARY KEY,
    CityName NVARCHAR(100),
    CountryID INT,
    FOREIGN KEY (CountryID) REFERENCES Countries(CountryID)
);
GO
INSERT INTO Cities (CityID, CityName, CountryID)
VALUES
(1, '����', 1),
(2, '�����', 1),
(3, '���������', 2),
(4, '���-����', 2),
(5, '������', 3),
(6, '�������', 3), 
(7, '�����', 4),
(8, '�������', 4), 
(9, '������', 5),
(10, '������', 5), 
(11, '�����', 6),
(12, '�����', 6), 
(13, '������', 7),
(14, '��������', 7),
(15, '�����', 8),
(16, '������', 8), 
(17, '����', 9),
(18, '������', 9), 
(19, '��������', 10),
(20, '���-�����', 10);

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
(1, '������� �������', '1985-02-15', 'M', 'aleksey.smirnov@email.com', 1),
(2, '����� �������', '1992-07-24', 'F', 'maria.ivanova@email.com', 2),
(3, '����� ��������', '1988-03-30', 'M', 'igor.nikolaev@email.com', 3),
(4, '����� �������', '1990-11-12', 'F', 'olga.petrova@email.com', 4),
(5, '������� ������', '1975-05-29', 'M', 'dmitry.kozlov@email.com', 5),
(6, '����� ��������', '1982-08-21', 'F', 'elena.sokolova@email.com', 6),
(7, '������ ��������', '1995-01-14', 'M', 'sergey.mikhailov@email.com', 7),
(8, '���� ���������', '1987-04-17', 'F', 'anna.kuznetsova@email.com', 8),
(9, '������� ��������', '1979-12-02', 'M', 'nikolay.vasilyev@email.com', 9),
(10, '������� ��������', '1993-09-10', 'F', 'tatiana.morozova@email.com', 10),
(11, '�������� �����', '1984-07-23', 'M', 'vladimir.yershov@email.com', 11),
(12, '�������� ��������', '1978-02-14', 'F', 'svetlana.nikitina@email.com', 12),
(13, '����� �������', '1991-03-28', 'M', 'anton.zakharov@email.com', 13),
(14, '���� ��������', '1994-10-19', 'F', 'yulia.semenova@email.com', 14),
(15, '����� ����������', '1986-06-07', 'M', 'pavel.vinogradov@email.com', 15),
(16, '����� �������', '1981-12-30', 'F', 'irina.miheeva@email.com', 16),
(17, '���� �������', '1997-08-25', 'M', 'egor.andreev@email.com', 17),
(18, '������� ��������', '1983-01-16', 'F', 'natalya.rybakova@email.com', 18),
(19, '����� �������', '1976-04-22', 'M', 'artem.kozyrev@email.com', 19),
(20, '�������� ������', '1995-07-05', 'F', 'viktoria.guseva@email.com', 20);

CREATE TABLE ProductInterests (
    InterestID INT PRIMARY KEY,
    InterestName NVARCHAR(100)
);
GO
INSERT INTO ProductInterests (InterestID, InterestName)
VALUES
(1, '�������'),
(2, '�����'),
(3, '���������� ������'),
(4, '�����������'),
(5, '������'),
(6, '�������'),
(7, '������'),
(8, '���������'),
(9, '�����������'),
(10, '��������������');


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
(1, 1, 1, '2024-04-01', '2024-04-30', '������ �� ������� �� 20% � ������!'),
(2, 2, 2, '2024-05-01', '2024-05-15', '���������� ���� - ���� ���, ������ � �������!'),
(3, 3, 3, '2024-06-01', '2024-06-30', '������ �� ���������� ������ �� 25%'),
(4, 4, 4, '2024-07-01', '2024-07-20', '������ ���������� �����������!'),
(5, 5, 5, '2024-08-01', '2024-08-31', '������ �� �������� �� 30%!'),
(6, 6, 6, '2024-09-01', '2024-09-15', '������� ��� ����� �� ������� 20%!'),
(7, 7, 7, '2024-10-01', '2024-10-31', '������� ��������� ������ - ������ �� 25%!'),
(8, 8, 8, '2024-11-01', '2024-11-30', '��������� � ���������� �� �������� �� 20%!'),
(9, 9, 9, '2024-12-01', '2024-12-31', '����������� ����������� �� ����������� �����!'),
(10, 10, 10, '2024-01-01', '2024-01-20', '�������������� �� ������� 15% � ����� ����!');

INSERT INTO SpecialOffers (PromotionID, InterestID, CountryID, StartDate, EndDate, PromotionDetails)
VALUES
(11, 10, 1, '2024-02-01', '2024-02-28', '������ ���������� ��������������� �� �������� �� 30%!'),
(12, 9, 2, '2024-03-01', '2024-03-20', '�������� ������ �� ����������� - �� 50%!'),
(13, 8, 3, '2024-04-10', '2024-04-30', '������ �� ��������� ��� ��������� ����������!'),
(14, 7, 4, '2024-05-01', '2024-05-15', '����� ��������� ������ - ����������� ����!'),
(15, 6, 5, '2024-06-01', '2024-06-20', '������ ������� ��� ��������� ����������� �� �������!'),
(16, 5, 6, '2024-07-15', '2024-08-15', '������ ��� ���� � ���� - ��������� ����������� ��� ����!'),
(17, 4, 7, '2024-08-01', '2024-08-31', '������������ ������ �� ����������� �� 40%!'),
(18, 3, 8, '2024-09-05', '2024-09-25', '������� ���������� ���������� �������!'),
(19, 2, 9, '2024-10-01', '2024-10-31', '������� ������� - ������ � �������!'),
(20, 1, 10, '2024-11-10', '2024-11-30', '������ �������: ������� �� ����� ������ ����� ����!');

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
