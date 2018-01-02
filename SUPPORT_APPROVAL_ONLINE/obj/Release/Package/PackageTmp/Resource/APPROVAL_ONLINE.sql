CREATE DATABASE SUPPORT_APPROVAL_ONLINE COLLATE  Vietnamese_100_CS_AS;
go

use SUPPORT_APPROVAL_ONLINE;
go
CREATE TABLE tbl_Group(
	group_Id int IDENTITY(1,1) PRIMARY KEY,
	group_Name nvarchar(50) NOT NULL,
);
CREATE TABLE tbl_Permission(
	permission_Id int IDENTITY(1,1) PRIMARY KEY,
	allow varchar(50)
);
go
CREATE TABLE tbl_User(
	U_Id int IDENTITY(1,1)  PRIMARY KEY,
	group_Id int,
	permission_Id int,
	U_username varchar(20) NOT NULL,
	U_password varchar(50) NOT NULL,
	U_fullname nvarchar(100) NOT NULL,
	U_phone varchar(20),
	U_email varchar(50),
	U_create_at nvarchar(100),
	FOREIGN KEY(group_Id) REFERENCES tbl_Group(group_Id),
	FOREIGN KEY(permission_Id) REFERENCES tbl_Permission(permission_Id),
);
go
CREATE TABLE tbl_Customer(
	customer_Id int IDENTITY(1,1) PRIMARY KEY,
	customer_Name nvarchar(100) NOT NULL
);
go
CREATE TABLE tbl_Request(
	id int IDENTITY(1,1) PRIMARY KEY,
	U_Id_Create int NOT NULL,--ID của người tạo yêu cầu
	U_Id_Approval int NOT NULL,--ID của người xác nhận yêu cầu
	U_Id_Dept_MNG int,--ID của trưởng bộ phận
	U_Id_LCA_Leader int,--ID của Leader LCA
	U_Id_LCA_MNG int,--ID của MNG LCA
	U_Id_FM int,--ID của phó giám đốc 
	U_Id_GD int,--ID của giám đốc
	customer_Id int NOT NULL,--Mã khách hàng
	quantity int NOT NULL,--Số lượng
	dealLine Date NOT NULL,--Ngày mong muốn
	title nvarchar(200),--Tiêu đề 
	increaseProductivity bit NOT NULL, --tăng năng suất
	newModel bit NOT NULL,
	increaseProduction bit NOT NULL, --Tăng sản lượng
	improve bit NOT NULL, --Cải tiến chất lượng
	_5s bit NOT NULL,
	checkJig bit NOT NULL, --kiểm tra Jig mòn
	reducePeple int, --Cắt giảm người
	errorContent nvarchar(200), -- Nội dung lỗi
	currentError varchar(10), -- Tỷ lệ lỗi hiện tại
	afterError varchar(10), --Tỷ lệ lỗi sau cải tiến
	cost_Savings int, --Tiết kiệm chi phí
	other nvarchar(100), --Khác
	pay nvarchar(100),--người chi trả
	model varchar(100),
	pcb varchar(100),
	contentDetail ntext,--Nội dung chi tiết
	cost int,--Chi phí
	date_Create Date,--Ngày khởi tạo
	date_Update Date,--Ngày cập nhật
	date_Received Date,-- Ngày nhận
	date_Finish Date, --ngày trả
	file_upload nvarchar(50),--Nội dung tải lên
	file_upload_update nvarchar(50),--Nội dung cập nhật tải lên
	costDetail_upload nvarchar(50),--Chi tiết bản báo giá
	FOREIGN KEY(customer_Id) REFERENCES tbl_Customer(customer_Id),
	FOREIGN KEY(U_Id_Create) REFERENCES tbl_User(U_Id),
);
go
insert into tbl_Group values('Support');
insert into tbl_Group values('MNG-LCA');
insert into tbl_Group values('Automation-LCA');
insert into tbl_Group values('Jig/Palet-LCA');
insert into tbl_Group values('Table/Shelf-LCA');
insert into tbl_Group values('Plastic tray-LCA');
insert into tbl_Group values('MC');
insert into tbl_Group values('PC');
insert into tbl_Group values('PD1');
insert into tbl_Group values('PE');

insert into tbl_Permission values('root');
insert into tbl_Permission values('admin');
insert into tbl_Permission values('user');

insert into tbl_User values ('1','1','admin','admin',N'Phạm Văn Quyết','2051','quyetphamit@gmail.com','Quyetphamit');
insert into tbl_User values ('2','2','sonvv','sonvv',N'Vũ Văn Sơn','9999','sonvv@umcvn.com','Quyetphamit');
insert into tbl_User values ('3','2','haint','haint',N'Nguyễn Thanh Hải','8888','haint@umcvn.com','Quyetphamit');
insert into tbl_User values ('4','2','thancv','thanhcv',N'Cao Văn Thanh','5555','thanhcv@umcvn.com','Quyetphamit');
insert into tbl_User values ('5','2','hangpv','hangpv',N'Phạm Văn Hằng','6666','hangpv@umcvn.com','Quyetphamit');
insert into tbl_User values ('6','2','hoannh','hoannh',N'Nguyễn Hữu Hoan','5555','hoannh@umcvn.com','Quyetphamit');
insert into tbl_User values ('7','3','tieptt','tieptt',N'Tiệp TT','4444','tieptt@umcvn.com','Quyetphamit');
insert into tbl_User values ('8','3','ngantp','1998',N'Nguyễn Thị Phương Nga','6789','ngalisa@gmail.com','Quyetphamit');
insert into tbl_User values ('9','3','dungtt','1992',N'Trần Thùy Dung','2345','dungtt@gmail.com','Quyetphamit');
insert into tbl_User values ('10','2','kaneco','kaneco','KaNeCo','2345','kaneco@umcvn.com','Quyetphamit');
insert into tbl_User values ('10','3','huongnt','huongnt',N'Nguyễn Thị Hương','5678','huongnt@umcvn.com','Quyetphamit');

insert into tbl_Customer values('Brother');
insert into tbl_Customer values('Fuji');
insert into tbl_Customer values('Nichicon');
insert into tbl_Customer values('Toyodenso');
insert into tbl_Customer values('Yokowo');
insert into tbl_Customer values('Murata');
insert into tbl_Customer values('Kyocera');
insert into tbl_Customer values('Honda');
insert into tbl_Customer values('HLDS');
select * from tbl_Customer;
select * from tbl_Request;
select * from tbl_User;
select * from tbl_Permission;
select * from tbl_Group;
select * from tbl_Group,tbl_User
where tbl_User.group_Id = tbl_Group.group_Id