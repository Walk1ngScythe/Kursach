SELECT id_order,username,model,start_date,expiration_date,start_point_id,end_point_id
FROM orders 
INNER JOIN Cars ON (id_rented_car = id_car) 
INNER JOIN Users ON (id_customer = id_user)
where id_rented_car in (select id_car from dbo.Cars where model = 'Hyndai Accent')