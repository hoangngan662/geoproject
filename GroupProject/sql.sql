use Trip
create table Place(
	id int identity(1,1) primary key,
	name nvarchar(100) not null,
	location Geometry not null,
	image nvarchar(100),
	keywords nvarchar(100)
)
insert into Place (name, location, image, keywords) values
(N'Thiền viện Trúc Lâm Phương Nam', geometry::STGeomFromText('POINT(105.703923 9.990357)', 0),'thienvien.jpg','thien vien vien truc lam phuong nam'),
(N'Khu Du Lịch Sinh Thái Ông Đề', geometry::STGeomFromText('POINT(105.707766 9.991795)', 0),'ongde.jpg','ong de'),
(N'Du lịch sinh thái vườn trái cây Phi Yến', geometry::STGeomFromText('POINT(105.694891 9.971439)', 0),'phiyen.jpg','phi yen'),
(N'Cồn Sơn', geometry::STGeomFromText('POINT(105.746047 10.085012)', 0),'conson.jpg','con son'),
(N'Bảo Gia Trang Viên', geometry::STGeomFromText('POINT(105.758496 9.960776)', 0),'baogiatrang.jpg','bao gia trang vien'),
(N'Cồn Ấu', geometry::STGeomFromText('POINT(105.803533 10.031590)', 0),'conau.jpg','con au'),
(N'Vườn Sinh Thái Ba Láng', geometry::STGeomFromText('POINT(105.739620 9.977994)', 0),'balang.jpg','ba lang'),
(N'Khu du lịch Cần Thơ Hoa Sứ', geometry::STGeomFromText('POINT(105.590375 10.134672)', 0),'hoasu.jpg','hoa su'),
(N'Vườn cò Bằng Lăng', geometry::STGeomFromText('POINT(105.505518 10.281463)', 0),'banglang.jpg','vuon co bang lang'),
(N'Vườn sinh thái Xẻo Nhum', geometry::STGeomFromText('POINT(105.776990 9.997829)', 0),'xeonhum.jpg','xeo nhum')



create table Account(
	id int identity(1,1) primary key,
	username nvarchar(50) not null,
	password nvarchar(50) not null
)
insert into Account (username, password) values ('admin','admin'),('hngan','123456'),('ttrang','123456')

create table Post(
	id int identity(1,1) primary key,
	title nvarchar(50) not null,
	content nvarchar(1000) not null,
	create_time datetime not null,
	id_author int not null,
	id_place int not null,
	foreign key (id_author) references Account(id),
	foreign key (id_place) references Place(id),
)




