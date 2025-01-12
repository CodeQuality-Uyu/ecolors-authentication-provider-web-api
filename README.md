# Authentication Provider

El servidor de autenticacion es una Web Api RestFull desarrollada en .NET usando EfCore como ORM para conectarse a dos bases de datos SQL. Una base de datos (AuthProvider) guarda la informacion de las cuentas, tenants, apps, roles, permisos, etc. Luego esta la base de datos (IdentityProvider) que guarda las credenciales de las cuentas de forma independiente a las cuentas. Esta separacion de informacion permite mayor flexibilidad de configuracion para la data sensible de la no sensible.

Esta web api es multi tenant y multi app, lo que significa que multiples tenant podran gestionar multiples aplicaciones propias dentro del sistema, filtrando la data por tenant y por app.

Esto es util para un tenant que tiene multiples app que se quiere que utilicen el mismo mecanismo de autenticacion y poder cruzar las cuentas entre las apps del mismo tenant.

## Definicion de entidades

### Empresa (Tenant)
Son las empresas registradas en el sistema que quieren usar los servicios de esta webapi para proveer un medio de autenticacion independiente a sus aplicaciones

### Aplicacion (App)
Son las aplicaciones de los tenant que quieren proveerles un medio de autenticacion y autorizacion, pudiendo desligar este diseño de los diseños de las aplicaciones.

### Roles de las cuentas (Role)
Los roles son agrupadores de permisos para poder autorizar a una cuenta en si puede o no acceder a un recurso controlado. Los roles no son un medio de verificacion de permisos, los permisos son este medio.
Los roles son unicos para cada aplicacion, esto quiere decir que si un tenant tiene dos aplicaciones, estos no pueden compartir los mismos roles ya que los permisos en la aplicacion van a ser diferentes.

### Permisos de las aplicaciones (Permission)
Los permisos son el medio para verificar si una cuenta puede o no acceder al recurso controlado. Estos son unicos por cada aplicacion, esto implica que un mismo permiso no puede aparecer en aplicaciones diferentes del mismo tenant.

### Invitaciones a personas (Invitation)
Las invitaciones son a futuras cuentas con un rol asignado a una app determinada del tenant.

### Reseteo de contraseña (ResetPassword)
Son solicitudes para reiniciar la contraseña de una cuenta que no puede loguearse. Esta solicitud requiere que la persona pueda acceder a su email personal con el cual se registro para poder obtener un codigo unico y asi poder resetear su contraseña sin estar autenticado.

### Cuenta de usuario (Account)
Los usuarios se pueden registrar en aplicaciones precargadas anteriormente en el sistema de un tenant.

### Sesiones (Session)
Son las sesiones activas de las cuentas. Para que un usuario pueda loguearse a una app, este a parte de enviar sus credenciales debera de proveer el identificador de la app a la cual se quiere loguear y este debera ser de una app en donde la cuenta este registrado. En caso de que no se provea, se utilizara la aplicacion establecida como por defecto en la que pertenezca la cuenta.

## Para ejecutar
Para correr la web api con docker ejecutar el siguiente comando en la ruta raiz del repo
```
docker-compose up --build
```

Una vez ejecutado con exito el comando, se debio de haber creado dos contenedores bajo una misma red, un contenedor para las bases de datos y otro para la web api. El ambiente configurado en el docker compose tiene seteado para que no sea tratado como produccion, lo que significa que al haberse ejecutado por primera vez, las migraciones de ambas bases de datos se debieron de ejecutar automatiamente. Esto implica que al parar la ejecucion de los contenedores y volverlos a iniciar, se va a consultar si quedan migraciones por aplicar, en caso de que si, las bases se van actualizar automaticamente. Esto facilita el mantenimiento de la base y la seguridad de que siempre se este trabajando con la ultima version de la base de datos.

Esta configuracion se puede desactivar si la variable de ambiente `ASPNETCORE_ENVIRONMENT` se setea con el valor `Production`. 

Por defecto tiene desactivada la opcion de que se simule la autenticacion teniendo la variable de ambiente `Authentication__Fake__IsActive` con el valor `false`. Esta variable solo deberia de ser `true` en ambientes locales, para simular la autenticacion lo que implica poder saltearse la autenticacion sin tener que depender de cuentas con los permisos o roles con los que se desea probar. Este mecanismo permite agilidad a la hora de desarrollo de la web api. Esta variable de ambiente no es necesario setearla si el ambiente es `Production`.

## Seed data
La web Api inicia con una cuenta semilla, un tenant semilla, la app de Auth Provier Web Api como default dentro del tenant semilla, 3 roles y los permisos para Auth Provider Web Api.

### Cuenta semilla
```json
{
  "email":"seed@cq.com",
  "password":"!12345678"
}
```
Esta cuenta permite realizar las siguientes operaciones:
- Obtener los roles creados dentro del tenant (`getall-role`)
- Crear una cuenta a otra persona en la plataforma dentro del tenant perteneciente a la cuenta semilla (`createcredentialsfor-account`)
- Transferir el tenant a otra cuenta (`transfertenant-me`)

### Tenant semilla
```json
{
  "name": "Semilla"
}
```
A este tenant se le debe de cambiar el nombre y ser transferido a la cuenta que va a ser el dueño de la app.

### App Auth Provider Web Api
Es la app que representa el servidor de autenticacion para poder realizar operaciones especificas solo para esta aplicacion. El servidor de autenticacion permite gestionar varias aplicaciones de los tenants asi pudiendo gestionar los permisos de acceso de los recursos de las aplicaciones de las cuentas creadas en el servidor.

```json
{
  "name": "Auth Provider Web Api"
}
```

### Roles semilla
Existen 3 roles, estos roles solo pueden ser usados en la app `Auth Provider Web Api`

- Seed (este rol solo sirve para la cuenta semilla, se deberia de eliminar posteriormente haber usado la cuenta semilla)
  
- Tenant Owner (las cuentas con este rol pueden acceder a recursos para gestionar las aplicaciones dentro del tenant y el tenant usando la app Auth Provider Web Api)
  
- Auth Provider Api Owner (las cuentas con este rol pueden acceder a recursos exclusivos dentro de la app Auth Provider Web Api)

### Permisos
Los permisos son los recursos y opciones a los cuales se les quiere restringir el acceso a las cuentas dentro de una app. Los permisos pertenecen unicamente a una app.

## Inicios con el servidor de autenticacion

### Paso 1
Loguearse con la cuenta semilla

### Paso 2
Crear una cuenta para otra persona con el rol de `Tenant Owner`

### Paso 3
Migrar el tenant de la cuenta semilla a la nueva cuenta creada

### Paso 4
Borrar la cuenta semilla

### Paso 5
Loguearse con la nueva cuenta con el rol de `Tenant Owner`

### Paso 6
Actualizar el nombre del tenant

### Paso 5
Crear una nueva app

### Paso 6
Crear permisos a la nueva app para restringir el acceso a los recursos

### Paso 7
Crear roles a la nueva app usando los permisos de la app


