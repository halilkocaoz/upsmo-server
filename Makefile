mig:
	cd ./UpsMo.Data && dotnet ef --startup-project ../UpsMo.WebAPI/ migrations add $(name) && cd ..
migrm:
	cd ./UpsMo.Data && dotnet ef migrations remove --startup-project ../UpsMo.WebAPI/ && cd ..
dbup:
	cd ./UpsMo.Data && dotnet ef --startup-project ../UpsMo.WebAPI/ database update && cd .. && make wr
r:
	cd ./UpsMo.WebAPI/ && dotnet run && cd ..
wr:
	cd ./UpsMo.WebAPI/ && dotnet watch run && cd ..
res:
	cd ./UpsMo.WebAPI/ && dotnet restore && cd ..