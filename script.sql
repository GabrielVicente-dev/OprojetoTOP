create database BancoProjetoTop;
use BancoProjetoTop;


create table Usuario(
id int primary key auto_increment,
Nome varchar (50) not null,
Email varchar (50) not null,
Senha varchar (50) not null
);

create table Produto(
IdProd int primary key auto_increment,
NomeProd varchar (50) not null,
DescProd varchar (100) not null,
quantidade int not null
);
