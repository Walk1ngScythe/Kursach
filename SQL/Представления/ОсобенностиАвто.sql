create view ��������������� as
SELECT model as '����������', name_car_feature as '�����������'
FROM feature_to_cars 
JOIN feature c ON (feature=id_feature)
JOIN Cars  ON (cars=id_car)
