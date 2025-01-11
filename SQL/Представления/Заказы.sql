ALTER VIEW Заказы AS
SELECT id_order, username AS 'Имя заказчика', model AS 'Модель', name_category AS 'Категория авто', start_date AS 'Дата начала заказа', expiration_date AS 'Дата окончания заказа', p.address AS 'Адрес начала', p1.address AS 'Адрес конца'
FROM orders 
INNER JOIN Cars ON (id_rented_car = id_car) 
INNER JOIN Users ON (id_customer = id_user)
INNER JOIN category_of_cars ON (category = id_category)
INNER JOIN points p ON (start_point_id = p.id_point)
LEFT JOIN points p1 ON (end_point_id = p1.id_point)
