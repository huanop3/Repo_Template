# Giai đoạn build: dùng SDK đầy đủ
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Đặt thư mục làm việc
WORKDIR /src

# Fix đường dẫn .csproj
COPY ["web_api_base.csproj", "./"]
RUN dotnet restore

# Copy toàn bộ source code
COPY . .

# Build và publish ứng dụng sang thư mục /app
RUN dotnet publish -c Release -o /app

# Giai đoạn runtime: dùng runtime nhẹ (không có SDK)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Tạo thư mục làm việc cho app
WORKDIR /app

# Copy file publish từ build stage
COPY --from=build /app .

# Uncomment các biến môi trường và port
# ENV ASPNETCORE_URLS=http://+:80
# EXPOSE 80

# Lệnh chạy app khi container start
ENTRYPOINT ["dotnet", "web_api_base.dll"]
