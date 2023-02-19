## Architecture
The Onion architecture have been chosen according to the requirements of the technical task due to avoiding meaningless overcomplication of the project.
There was not enougn information about app expanding plans for chosing looser, expandable, but more expensive and complicated solutions as CQRS or microservices.

Decisions about the complexity of certain services were made based on requirements. I think it's better to do only what a 'client' requests, and if it will be not enough - more time could be spent on improvements - it can helps save money and time.

## Time
- Estimated time: 5-6 hours
- Actual time: 6h25m

Things that are not marked in the technical task as needed, but could be added:
- Registration (approx.: 30-40 minutes) [example](https://github.com/the-17th-fox/vehicle-information-system/blob/e7a96ef55c8f0d62d1a1fdfe3bde4ea94f9532da/src/AccountsService/Services/AccountsSvc.cs#L85)
- Reorgonizing for Roles and Policies using (approx.: 1 hour) [example](https://github.com/the-17th-fox/CSARN-Microservice/blob/b134a646b6a25e6ddc3d1291c62b007220870b50/src/CSARN.AccountsMicroservice/Web/Extensions/ServicesCollectionExtension.cs#L21)
- FluentValidation also can be added, but I think it will be useless complexity (approx.: 30 minutes)
- UnitTests (20 minutes for MeetupsService, 50 minutes for Account+Tokens)

## How to use
#### Using the source code
In the root folder using any terminal: ```docker-compose up -d```

#### How to send requests
When started, API recieves at ```http://localhost:9999/api/{tokens|users|meetups}/{method}```

#### Auth
Use for login:
```json
{
  "email": "Administrator@mail.com",
  "password": "rootpass"
}
```
**Access token lifetime is set to 5 minute, it could be changed in the docker compose file to whatever you need, field ```Authentication__Jwt__AuthTokenLifetimeInMinutes: ```.**

#### Possible meetup creation:
```json
{
  "Title": "MeetupTitle",
  "Description": "Possible meetup description",
  "Location": "Vitebsk, Belarus",
  "StartsAt": "2022-10-17T19:01:55",
  "Speaker": "SpeakerName",
  "MeetupManager": "ManagerName"
}
```

#### View logs
Logs could be accessed at ```http://localhost:8081/db/logs```

#### PS.
Left all branches undeleted - maybe it will be needed in some way.
