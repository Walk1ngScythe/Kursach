alter view Заказы as
SELECT  id_order, username as 'Имя заказчика', model as 'Модель', name_category as 'Категория авто', start_date as 'Дата начала заказа', expiration_date as 'Дата окончания заказа', p.address as 'Адрес начала', p1.address 'Адрес конца'
FROM orders 
INNER JOIN Cars ON (id_rented_car = id_car) 
INNER JOIN Users ON (id_customer = id_user)
INNER JOIN category_of_cars ON (category = id_category)
INNER JOIN points p ON (start_point_id = p.id_point)
INNER JOIN points p1 ON (end_point_id = p1.id_point)