INSERT INTO Roles (name_role) VALUES 
('admin'),
('user'),
('manager');
INSERT INTO points (address) VALUES
('Ленина 92'),
('Кирова 52');
INSERT INTO category_of_cars (name_category)  VALUES
('Comfort'),
('Economy'),
('Business');
INSERT INTO feature (name_car_feature) VALUES
('Тонер'),
('Передний привод'),
('Задний привод'),
('Детское кресло'),
('Парктроник');
INSERT INTO Users (username, email, password, disabled_person, id_role) VALUES
('Иваныч','zxcghuel@gmail.com','123qwe321',0,2),
('Петрович','clownadmin@gmail.com','petr1995',0,1),
('Кирюха','kiraKill@gmail.com','kirakill52',1,2),
('Василёк','vasyanchik96@gmail.com','vanpis55',0,3);
INSERT INTO Cars (model,category,status,for_disabled_person,price) VALUES
('Mersedes E200',3,0,0,10000),
('Hyndai Accent',2,1,1,2000),
('Ford Focus',1,0,0,4500),
('Opel Vectra B',1,0,0,3500),
('BMW M5',3,1,0,10000),
('VAZ 21053',2,0,0,1000);
INSERT INTO feature_to_cars  VALUES
(1,1),
(1,4),
(1,5),
(2,2),
(2,3),
(2,4),
(2,5),
(3,6),
(3,1),
(4,2),
(4,4),
(4,5),
(5,1),
(5,3),
(5,5);
INSERT INTO orders (id_customer,id_rented_car,start_date,expiration_date,start_point_id,end_point_id) VALUES
(1,2,'2024-01-01 12:00:00',NULL,1,NULL),
(1,2,'01-12-2023 12:00:00','01-12-2023 14:00:00',1,2),
(1,4,'14-11-2023 06:00:00','16-11-2023 21:00:00',1,2), 
(3,2,'21-05-2023 13:30:00','01-06-2023 21:00:00',1,2),
(1,4,'21-04-2023 07:30:00','01-05-2023 15:00:00',1,2),
(3,1,'01-06-2023 22:00:00','22-06-2023 22:00:00',1,2);

