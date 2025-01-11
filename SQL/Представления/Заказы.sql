ALTER VIEW ������ AS
SELECT id_order, username AS '��� ���������', model AS '������', name_category AS '��������� ����', start_date AS '���� ������ ������', expiration_date AS '���� ��������� ������', p.address AS '����� ������', p1.address AS '����� �����'
FROM orders 
INNER JOIN Cars ON (id_rented_car = id_car) 
INNER JOIN Users ON (id_customer = id_user)
INNER JOIN category_of_cars ON (category = id_category)
INNER JOIN points p ON (start_point_id = p.id_point)
LEFT JOIN points p1 ON (end_point_id = p1.id_point)
