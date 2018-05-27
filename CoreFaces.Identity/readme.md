IdentityAdmin projesi şu an için durduruldu çünkü gerek yok gibi duruyor.
Yeni bir App user açılacağı zaman test projesi içinden aç ve yola devam edilecek zaman kaybına gerek yok.

1. Mongo DB docker da oluşturulur <br />
docker run --name mongo-server -p 27017:27017 -d mongo:2.4 --auth

2. oluþturulan mongo-server içine admin olarak bağlanılır <br />
docker exec -it mongo-server mongo admin

3. Database admin user oluşturulur. <br />
db.createUser( { user: "MyUser",
          pwd: "123456",
          roles: [ "userAdminAnyDatabase",
                   "dbAdminAnyDatabase",
                   "readWriteAnyDatabase"

] } )

4. Bir database oluşturulur örn. Identity ve buna bir user tanımlanır. örnek user oluþturma resmi : <br /> 
![alt text](/readme/mongo-user.PNG)
<br>
<br>
5. Docker işlemleri <br>
 Örnek Dockerfile dosyasına <a href="https://github.com/altunbas-huseyin/IdentityV2/blob/master/Identity/Dockerfile"> /Identity/Identity/Dockerfile</a> yolundan ulaşabilirsiniz. 
<br>

docker imajı hazırlar <br>
docker build -t identity-app . <br>
hazırlanan docker imajından bir örnek/container oluşturur <br>
docker run --name=identity  -p 5000:5000 identity-app <br>
ve mutlu son  <b>docker start identity-app</b> komutu ile kontainer ayağa kaldırılır.

6. Roller 
<ul>
<li> SystemAdmin : Tüm sistemi ve kullanıcıları yönetebilen kullanıcı. </li>
<li> AppAdmin    : Yeni oluşturulan bir proje,app olarak isimlendirmede  sıkıntı yok sanırım. parse.com yada farklı bir servise üye olmak yerine bu sisteme üye olan kişinin hesabı. Bu apiyi kullanarak yazılım geliştirebilir. </li>
<li> AppUser     :  AppAdmin rolüne sahip üye tarafından eklenen kullanıcı. </li>
</ul>



7. Deployment
Daha önceden çalışan bir identity adında container olduğu için önce identity isimli container'ı durduruyoruz
<b> docker stop identity </b>
sonra identity isimli container'ı siliyoruz
<b>docker rm identity</b>

docker image siliniyor, silinmez ise bir önceki image ı görüyor ve image oluşturulurken image için kopyalanan dosyaları kullanıyor. kısacası image mutlaka silinmeli
docker rmi identity-app
docker build -t identity-app .

sonra tekrar identity container oluşturulur ve çalıştılır aşağıdaki komut ile.
docker run --name=identity  -p 5000:5000 identity-app
container durdurulup çalıştırılabilir
docker stop identity
docker start identity

toplu komut
docker stop identity && 
docker rm identity && 
docker rmi identity-app &&
docker build -t identity-app . &&
docker run --name=identity  -p 5000:5000 identity-app &&
docker start identity 

8. Backup<br>
docker cp b73e69ebcbeb:/backup/1.txt /srv/users/serverpilot/temphuso/1.txt #docker instance içine dosya koplalar<br>
docker cp b73e69ebcbeb:/backup /srv/users/serverpilot/temphuso/ #docker inctance içindeki dosyayı dışarı kaydeder, yani ana makinadaki bir klasöre kaydeder.