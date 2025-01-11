create view ќсобенностијвто as
SELECT model as 'јвтомобиль', name_car_feature as 'ќсобенность'
FROM feature_to_cars 
JOIN feature c ON (feature=id_feature)
JOIN Cars  ON (cars=id_car)
