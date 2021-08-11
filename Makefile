mig:
	cd ./UpMo.Data && dotnet ef --startup-project ../UpMo.WebAPI/ migrations add $(name) && cd ..
migrm:
	cd ./UpMo.Data && dotnet ef migrations remove --startup-project ../UpMo.WebAPI/ && cd ..
dbup:
	cd ./UpMo.Data && dotnet ef --startup-project ../UpMo.WebAPI/ database update && cd ..
r:
	cd ./UpMo.WebAPI/ && dotnet run && cd ..
wr:
	cd ./UpMo.WebAPI/ && dotnet watch run && cd ..
res:
	cd ./UpMo.WebAPI/ && dotnet restore && cd ..