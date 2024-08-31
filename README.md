# EShopMicroservices
This is step-by-step development of reference microservices architecture ensuring that high-scale and high-availability services using .NET 8.
There is a couple of microservices which implemented E-Commerce Microservices, those are Catalog, Basket, Discount, Ordering, YarpApiGateway microservices and Shopping.Web Client Application. These microservices data's will store NoSQL and Relational databases with communicating over gRPC and RabbitMQ Event Driven Communication and also using Yarp API Gateway for client operations.

![EshopMicroservice drawio(1)](https://github.com/user-attachments/assets/2483daa1-631d-4840-8c1e-cc1daaec1c94)

🔹𝐂𝐚𝐭𝐚𝐥𝐨𝐠 𝐌𝐢𝐜𝐫𝐨𝐬𝐞𝐫𝐯𝐢𝐜𝐞
Using ASP.NET Core Minimal APIs
Vertical Slice Architecture implementation with Feature folders
CQRS implementation using MediatR library with Pipeline Behaviors
Use Marten library for .NET Transactional Document DB on PostgreSQL

🔹𝗕𝗮𝘀𝗸𝗲𝘁 𝗠𝗶𝗰𝗿𝗼𝘀𝗲𝗿𝘃𝗶𝗰𝗲
Using Redis as a Distributed Cache over basketdb 
Implements Proxy, Decorator and Cache-aside patterns
Consume Discount Grpc Service for inter-service sync communication to calculate product final price
Publish BasketCheckout Queue with using MassTransit and RabbitMQ

🔹𝗗𝗶𝘀𝗰𝗼𝘂𝗻𝘁 𝗺𝗶𝗰𝗿𝗼𝘀𝗲𝗿𝘃𝗶𝗰𝗲
Exposing Grpc Services with creating Protobuf messages
SQLite database connection and containerization 
N-Layer Architecture implementation 

🔹𝗢𝗿𝗱𝗲𝗿𝗶𝗻𝗴 𝗠𝗶𝗰𝗿𝗼𝘀𝗲𝗿𝘃𝗶𝗰𝗲 
Implementing DDD, CQRS, and Clean Architecture with using Best Practices 
Raise and Handle Domain Events & Integration Events
Entity Framework Core Code-First Approach, Migrations
Consuming RabbitMQ BasketCheckout event queue with using MassTransit-RabbitMQ Configuration
