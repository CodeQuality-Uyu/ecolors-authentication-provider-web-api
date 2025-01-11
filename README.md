# Authentication Provider

## Para ejecutar
Para correr la web api con docker ejecutar el siguiente comando en la ruta raiz del repo
```
docker-compose up --build
```

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
- Obtener los roles creados dentro del tenant
- Crear una cuenta a otra persona en la plataforma dentro del tenant perteneciente a la cuenta semilla
- Transferir el tenant a otra cuenta

### Tenant semilla
```json
{
  "name": "Semilla"
}
```
A este tenant se le debe de cambiar el nombre y se transferido a la cuenta que va a ser el dueño de la app.

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


