## Table of contents
:one: [General info](#general-info) :memo:  
:two:  [Demo](#demo) 👆  
:three: [Authorize](#authorize) 🔓  
:four: [Features](#features) ⭐    
:five: [Technologies](#technologies) 🔧  
:six: [API endpoints](#api-endpoints) ▶️
## :one: General info
### Waste segregation

is application that allows send information to town hall in your city how 
many waste bags you need. Its helps to minimize the amount of plastic 
garbage bags and save money. Application can be use in any city.


## :two: Demo  

The application is working on ![icons8-azure](https://user-images.githubusercontent.com/57062649/207971054-aa4975de-ff6a-423b-8b2b-6fe02339eef7.svg) You can try it on ![icons8-postman-is-the-only-complete-api-development-environment-28](https://user-images.githubusercontent.com/57062649/208315141-c05f9cb8-09b6-48c8-9012-ce4a8d2c4698.png) or
![swagger](https://user-images.githubusercontent.com/57062649/207968847-10b293bf-9571-4d3c-ae95-eb8d2f4165be.svg) 
https://wastesegregation.azurewebsites.net/swagger  

## :three: Authorization  
You can authorize in ![icons8-postman-is-the-only-complete-api-development-environment-28](https://user-images.githubusercontent.com/57062649/208315141-c05f9cb8-09b6-48c8-9012-ce4a8d2c4698.png) or ![swagger](https://user-images.githubusercontent.com/57062649/207968847-10b293bf-9571-4d3c-ae95-eb8d2f4165be.svg)  
Authorization in ![swagger](https://user-images.githubusercontent.com/57062649/207968847-10b293bf-9571-4d3c-ae95-eb8d2f4165be.svg)  
:one: Login ```POST/api/Identity/Login```    
:two: Copy token  
:three: Click Authorize ![swagger authorize](https://user-images.githubusercontent.com/57062649/208315552-94c5b81d-ca74-4cf0-91e6-9a79310c7967.jpg)  
:four: Paste token in Value textbox and click Authorize   
![swagger authorize value](https://user-images.githubusercontent.com/57062649/208315651-ba6b6d1e-2502-4f03-851b-24cb5dd8c5e9.jpg)  


## :four: Features

:arrow_forward: **As admin you can**
- Registration
- Get all real estates
- Pagination, sorting and filtering

:arrow_forward: **As user you can**

- Registration
- Create real estate
- Update reale estate you created
- Delete reale estate you created
- Get by id real estate you created
- Pagination, sorting and filtering


## :five: Technologies

- .NET 6.0
- AspNetCore.Identity 2.2.0
- AutoMapper 12.0.0
- C# 10.0
- EntityFrameworkCore 6.0.10
- FluentAssertions 6.8.0
- FluentValidations 11.2.2
- Moq 4.18.2
- NLog 5.0.5
- XUnit 2.4.2


## :six: API endpoints

### Identity
#### Register as a user
![#49cc90](https://placehold.co/20x20/49cc90/49cc90.png)   
`POST/api/Identity/Register`

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
|           | `string` | **Required**. username     |
|           | `string` | **Required**. email        |
|           | `string` | **Required**. password     |

#### Register as a admin
![#49cc90](https://placehold.co/20x20/49cc90/49cc90.png)  
`POST/api/Identity/RegisterAdmin`

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
|           | `string` | **Required**. username     |
|           | `string` | **Required**. email        |
|           | `string` | **Required**. password     |

#### Login
![#49cc90](https://placehold.co/20x20/49cc90/49cc90.png)  
`POST/api/Identity/Login`

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
|           | `string` | **Required**. email        |
|           | `string` | **Required**. password     |

### RealEstates
#### Get all real estates
![#61affe](https://placehold.co/20x20/61affe/61affe.png)  
`GET/api/RealEstates`

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
|           | `int`    | PageNumber                 |
|           | `int`    | PageSize                   |
|           | `string` | SortField                  |
|           | `boolean`| Ascending                  |
|           | `string` | FilterBy                   |

#### Create real estate
![#49cc90](https://placehold.co/20x20/49cc90/49cc90.png)  
`POST/api/RealEstates`

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
|           | `string` | **Required**. street       |
|           | `string` | **Required**. streetNumber |
|           | `string` | **Required**. postCode     |
|           | `string` | **Required**. city         |

#### Get real estate by id
![#61affe](https://placehold.co/20x20/61affe/61affe.png)  
`GET/api/RealEstates/{id}`

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `id`      | `int`    | **Required**. id           |

#### Update real estate
![#fca130](https://placehold.co/20x20/fca130/fca130.png)  
`PUT/api/RealEstates`

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `id`      | `int`    | **Required**. id           |
|           | `string` | **Required**. street       |
|           | `string` | **Required**. streetNumber |
|           | `string` | **Required**. postCode     |
|           | `string` | **Required**. city         |

#### Delete real estate by id
![#f93e3e](https://placehold.co/20x20/f93e3e/f93e3e.png)  
`DELETE/api/RealEstates/{id}`

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `id`      | `int`    | **Required**. id           |

#### Get fields you can sort by
![#61affe](https://placehold.co/20x20/61affe/61affe.png)  
`GET/api/RealEstates/GetSortFields`


### WasteBags
#### Give amount of waste bags you need
![#49cc90](https://placehold.co/20x20/49cc90/49cc90.png)  
`POST/api/RealEstate/{realEstateId}/wasteBags`

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `realEstateId`| `int`| **Required**. realEstateId |
|           | `int`    | blueBag                    |
|           | `int`    | greenBag                   |
|           | `int`    | yellowBag                  |
|           | `int`    | brownBag                   |
