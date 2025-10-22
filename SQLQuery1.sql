select * from [employee];
select * from [user];
select * from [Address]

--insert into [Employee] ([Id],[AddressId], [EmployeeType], StartDate, EndDate, UserId)
--		values
--						(2, 1, 5, '08-24-2023', '12-12-2108', 3);
--update [user] set EmployeeId=null where Id=3;
--update [Employee] set EmployeeType=1 where Id=1;


--insert into [Address] ([street], [Number], City, PostalCode, Country) values ('Crapaurue', 11, 'Verviers', 4800, 'Belgium');
--insert into Owner ([Name], [AddressId], ContactName, PhoneNumber, Email)
--		values ('Zoo de anvers', 1, 'truc', '01452212', 'anvers@truc.be'),
--		 ('Pairidaiza', 1, 'michmuche', '087566321', 'atg@dd');
--insert into AnimalSpecies ([Name], [Description]) values
--			('Ocelot', 'Feline animal with big ears'),
--			('Elephant', 'Biggest terrestrial mammal'),
--			('Gorilla', 'Big monkey');

--insert into [Animal] ([Name], [Sex], [SpeciesId], [OwnerId], IsAvailable, BirthDate, RIPDate)
--		values		
--					('Lea', 2, 1, 1, 0, '10-06-2003', null),
--					('Georges', 3, 2, 1, 1, '01-01-2023', null),
--					('Noah', 4, 1, 1, 0, '11-11-1978', '06-05-2013');