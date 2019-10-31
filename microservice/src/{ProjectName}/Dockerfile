FROM mcr.microsoft.com/dotnet/core/sdk:3.0
COPY pub/ /root/
WORKDIR /root/
ENV ASPNETCORE_URLS="http://*:80"
EXPOSE 80/tcp
ENTRYPOINT ["dotnet", "{ProjectName}.dll"]