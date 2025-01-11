CREATE TABLE"Users" (
	"id_user" int NOT NULL UNIQUE,
	"username" varchar(15) NOT NULL,
	"email" varchar(30) NOT NULL,
	"password" varchar(15) NOT NULL,
	"disabled_person" bit NOT NULL,
	"id_role" int NOT NULL,
	PRIMARY KEY ("id_user")
);

CREATE TABLE "Roles" (
	"id_role" int NOT NULL UNIQUE,
	"name_role" varchar(30) NOT NULL,
	PRIMARY KEY ("id_role")
);

CREATE TABLE "Cars" (
	"id_car" int NOT NULL UNIQUE,
	"model" varchar(30) NOT NULL,
	"status" bit NOT NULL,
	"for_disabled_person" bit NOT NULL,
	"price" int NOT NULL,
	PRIMARY KEY ("id_car")
);

CREATE TABLE "orders" (
	"id_order" int NOT NULL UNIQUE,
	"id_customer" int NOT NULL,
	"id_rented_car" int NOT NULL,
	"start_date" datetime NOT NULL,
	"expiration_date" datetime,
	"start_point_id" int NOT NULL,
	"end_point_id" int,
	PRIMARY KEY ("id_order")
);

CREATE TABLE "categories" (
	"id_categories" int NOT NULL UNIQUE,
	"name_car_categories" varchar(30) NOT NULL,
	PRIMARY KEY ("id_categories")
);

CREATE TABLE "categories_to_cars" (
	"categories" int NOT NULL,
	"cars" int NOT NULL
);

CREATE TABLE "points" (
	"id_point" int NOT NULL UNIQUE,
	"address" varchar(30) NOT NULL,
	PRIMARY KEY ("id_point")
);

ALTER TABLE "Users" ADD CONSTRAINT "Users_fk5" FOREIGN KEY ("id_role") REFERENCES "Roles"("id_role");


ALTER TABLE "orders" ADD CONSTRAINT "orders_fk1" FOREIGN KEY ("id_customer") REFERENCES "Users"("id_user");

ALTER TABLE "orders" ADD CONSTRAINT "orders_fk2" FOREIGN KEY ("id_rented_car") REFERENCES "Cars"("id_car");

ALTER TABLE "orders" ADD CONSTRAINT "orders_fk5" FOREIGN KEY ("start_point_id") REFERENCES "points"("id_point");

ALTER TABLE "orders" ADD CONSTRAINT "orders_fk6" FOREIGN KEY ("end_point_id") REFERENCES "points"("id_point");

ALTER TABLE "categories_to_cars" ADD CONSTRAINT "categories_to_cars_fk0" FOREIGN KEY ("categories") REFERENCES "categories"("id_categories");

ALTER TABLE "categories_to_cars" ADD CONSTRAINT "categories_to_cars_fk1" FOREIGN KEY ("cars") REFERENCES "Cars"("id_car");
