alter view ������ as
SELECT  id_order, username as '��� ���������', model as '������', name_category as '��������� ����', start_date as '���� ������ ������', expiration_date as '���� ��������� ������', p.address as '����� ������', p1.address '����� �����'
FROM orders 
INNER JOIN Cars ON (id_rented_car = id_car) 
INNER JOIN Users ON (id_customer = id_user)
INNER JOIN category_of_cars ON (category = id_category)
INNER JOIN points p ON (start_point_id = p.id_point)
INNER JOIN points p1 ON (end_point_id = p1.id_point)