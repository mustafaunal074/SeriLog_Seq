# SeriLog_Seq
SeriLoglama işlemini Seq ile Görselleştirme

Bu projede Loglama işlemini Seq üzerinden görselleştirerek gerçekleştirdik.
Projenin içinde ekstra olarak console,debug ve file da loglama işlemleri gerçekleştirilmektedir.

Projede Seq ile görselleştrime için Seq uygulamasının yüklü olması lazım.
Ben projemde Docker ile ayağa kaldırdım. Dockerdan ayağa kaldırmak için aşağıdaki kodu run edebilirisiniz.

docker run --name seq -e ACCEPT_EULA=Y -p 5341:80 datalust/seq:latest


##Projede Kullandığım Nuget Package'lar

1.Serilog.Sinks.Console
2.Serilog.Sinks.Debug
3.Serilog.Sinks.File
4.Serilog.Sinks.Seq
