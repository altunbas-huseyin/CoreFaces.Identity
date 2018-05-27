FROM microsoft/dotnet:2.0.0-sdk-jessie
WORKDIR /app
ENV ASPNETCORE_URLS http://*:5000
EXPOSE 5000
ENTRYPOINT ["dotnet", "Identity.dll"]
#Identity uygulama dosyalar� container i�inde /app yoluna kopyalan�yor.
COPY . /app 
