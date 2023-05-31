# N5 Challenge Service Permission

Pending include description of service

### Software requirements

* Docker and Docker Compose
* NET 6

### Steps to use this api

1. Pending include details of steps for to use api
2. View message in topic
 - Get Container id of kafka
 ![Challenge](https://github.com/jordiny/n5-challenge-app-permission/assets/13501855/ad140d99-a7a5-42e9-a7d3-50d032e94581)

  - Execute commands
  - docker exec -it container-id sh
  - cd /opt/kafka_2.13-2.8.1/bin
  - kafka-topics.sh --list --bootstrap-server localhost:9092 (view topics)
  - kafka-console-consumer.sh --topic n5_permission_event_topic --from-beginning --bootstrap-server localhost:9092 (view messages)

## Features
- [x] Clean Architecture
- [x] CQRS with MediatR Library
- [x] Entity Framework Core - Code First
- [x] Repository Pattern
- [x] Unit of Work
- [x] MediatR Pipeline Logging & Validation
- [x] Serilog
- [x] Swagger UI
- [x] Custom Exception Handling Middlewares
- [x] Fluent Validation
- [x] Automapper
- [x] Kibana/ElasticSearch (Logs/Log Change)
- [x] Kafka Event bus
- [x] Unit Testing With xUnit
- [x] Integration Testing With NUnit

## Unit Testing And Integration Testing


![Challenge](https://github.com/jordiny/n5-challenge-service-permission/assets/13501855/c0267e02-d697-4401-a130-15d84af394ef)

## Test API

* ## Permission API 

![Challenge](https://github.com/jordiny/n5-challenge-app-permission/assets/13501855/81d6188b-0c2d-4abc-89f4-3b24696c8d20)

* ## Test Kibana/ELK

![Challenge](https://github.com/jordiny/n5-challenge-app-permission/assets/13501855/94b7fadf-561a-461c-93fa-ab75168d9f37)

* ## Test Kibana/ELK - Logs Service
![Challenge](https://github.com/jordiny/n5-challenge-app-permission/assets/13501855/d1a3e28a-fa1a-424a-9ec9-333ad7302798)

* ## Test Kibana/ELK - Log Change Permission
![Challenge](https://github.com/jordiny/n5-challenge-app-permission/assets/13501855/be2ea042-4dfe-49e5-910b-a0c9f9813823)


* ## Test Kafka

![Challenge](https://github.com/jordiny/n5-challenge-app-permission/assets/13501855/53f16cdb-2661-4f16-acb9-f518dd94d667)





