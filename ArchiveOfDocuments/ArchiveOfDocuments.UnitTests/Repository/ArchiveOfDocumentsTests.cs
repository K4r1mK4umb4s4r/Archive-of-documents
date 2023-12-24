using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace ArchiveOfDocuments.UnitTests.Repository
{
    public class ArchiveOfDocumentsTests
    {
        [TestFixture]
        [Category("Integration")]
        public class ArchiveOfDocumentsTests : RepositoryTestsBaseClass
        {
            [Test]
            public void GetAllEventsTest()
            {
                //prepare
                using var context = DbContextFactory.CreateDbContext();
                var type_event = new EventType()
                {
                    Name = "Опера",
                    ExternalId = Guid.NewGuid()
                };
                context.TypesEvents.Add(type_event);
                context.SaveChanges();

                var events = new EventEntity[]
                {
            new EventEntity()
            {
                TypeEventId=type_event.Id,
                Name="Руслан и Людмила",
                Desctiption="Потрясающая опера, исполняемая артистами Московского Большого театра",
                AgeViewer=6,
                ExternalId = Guid.NewGuid()
            },
            new EventEntity()
            {
                TypeEventId=type_event.Id,
                Name="Кармен",
                Desctiption="Потрясающая опера, исполняемая артистами Московского Большого театра",
                AgeViewer=12,
                ExternalId = Guid.NewGuid()
            },
                };
                context.Events.AddRange(events);
                context.SaveChanges();

                //execute
                var repository = new Repository<EventEntity>(DbContextFactory);
                var actualEvents = repository.GetAll();

                //assert        
                actualEvents.Should().BeEquivalentTo(events, options => options.Excluding(x => x.EventType));
            }
            [Test]
            public void GetAllEventsWithFilterTest()
            {
                //prepare
                using var context = DbContextFactory.CreateDbContext();
                var type_event = new EventType()
                {
                    Name = "Опера",
                    ExternalId = Guid.NewGuid()
                };
                context.TypesEvents.Add(type_event);
                context.SaveChanges();

                var events = new EventEntity[]
                {
            new EventEntity()
            {
                TypeEventId=type_event.Id,
                Name="Руслан и Людмила",
                Desctiption="Потрясающая опера, исполняемая артистами Московского Большого театра",
                AgeViewer=6,
                ExternalId = Guid.NewGuid()
            },
            new EventEntity()
            {
                TypeEventId=type_event.Id,
                Name="Кармен",
                Desctiption="Потрясающая опера, исполняемая артистами Московского Большого театра",
                AgeViewer=12,
                ExternalId = Guid.NewGuid()
            },
                };

                context.Events.AddRange(events);
                context.SaveChanges();
                //execute

                var repository = new Repository<EventEntity>(DbContextFactory);
                var actualEvents = repository.GetAll(x => x.Name == "Кармен").ToArray();

                //assert
                actualEvents.Should().BeEquivalentTo(events.Where(x => x.Name == "Кармен"), options => options.Excluding(x => x.EventType));
            }

            [Test]
            public void SaveNewEventTest()
            {
                //prepare
                using var context = DbContextFactory.CreateDbContext();
                //execute
                var type_event = new EventType()
                {
                    Name = "Опера",
                    ExternalId = Guid.NewGuid()
                };
                context.TypesEvents.Add(type_event);
                context.SaveChanges();

                var my_event = new EventEntity()
                {
                    TypeEventId = type_event.Id,
                    Name = "Кармен",
                    Desctiption = "Потрясающая опера, исполняемая артистами Московского Большого театра",
                    AgeViewer = 12,
                    ExternalId = Guid.NewGuid()
                };
                var repository = new Repository<EventEntity>(DbContextFactory);
                repository.Save(my_event);

                //assert
                var actualEvent = context.Events.SingleOrDefault();
                actualEvent.Should().BeEquivalentTo(my_event, options => options.Excluding(x => x.Id)
                                                                                .Excluding(x => x.ModificationTime)
                                                                                .Excluding(x => x.CreationTime)
                                                                                .Excluding(x => x.ExternalId)
                                                                                .Excluding(x => x.EventType));
                actualEvent.Id.Should().NotBe(default);
                actualEvent.ModificationTime.Should().NotBe(default);
                actualEvent.CreationTime.Should().NotBe(default);
                actualEvent.ExternalId.Should().NotBe(Guid.Empty);
            }

            [Test]
            public void UpdateEventTest()
            {
                //prepare
                using var context = DbContextFactory.CreateDbContext();
                var type_event = new EventType()
                {
                    Name = "Опера",
                    ExternalId = Guid.NewGuid()
                };
                context.TypesEvents.Add(type_event);
                context.SaveChanges();

                var my_event = new EventEntity()
                {
                    TypeEventId = type_event.Id,
                    Name = "Кармен",
                    Desctiption = "Потрясающая опера, исполняемая артистами Московского Большого театра",
                    AgeViewer = 12,
                    ExternalId = Guid.NewGuid()
                };
                context.Events.Add(my_event);
                context.SaveChanges();

                //execute

                my_event.Name = "Евгений Онегин";
                my_event.AgeViewer = 16;
                var repository = new Repository<EventEntity>(DbContextFactory);
                repository.Save(my_event);

                //assert
                var actualEvent = context.Events.SingleOrDefault();
                actualEvent.Should().BeEquivalentTo(my_event, options => options.Excluding(x => x.EventType));
            }

            [Test]
            public void DeleteEventTest()
            {
                //prepare
                using var context = DbContextFactory.CreateDbContext();
                var type_event = new EventType()
                {
                    Name = "Опера",
                    ExternalId = Guid.NewGuid()
                };
                context.TypesEvents.Add(type_event);
                context.SaveChanges();

                var my_event = new EventEntity()
                {
                    TypeEventId = type_event.Id,
                    Name = "Кармен",
                    Desctiption = "Потрясающая опера, исполняемая артистами Московского Большого театра",
                    AgeViewer = 12,
                    ExternalId = Guid.NewGuid()
                };
                context.Events.Add(my_event);
                context.SaveChanges();

                //execute

                var repository = new Repository<EventEntity>(DbContextFactory);
                repository.Delete(my_event);

                //assert
                context.Events.Count().Should().Be(0);
            }
            [Test]
            public void GetByIdTest_PositiveCase()
            {
                //prepare
                using var context = DbContextFactory.CreateDbContext();
                var type_event = new EventType()
                {
                    Name = "Опера",
                    ExternalId = Guid.NewGuid()
                };
                context.TypesEvents.Add(type_event);
                context.SaveChanges();

                var events = new EventEntity[]
                {
            new EventEntity()
            {
                TypeEventId=type_event.Id,
                Name="Руслан и Людмила",
                Desctiption="Потрясающая опера, исполняемая артистами Московского Большого театра",
                AgeViewer=6,
                ExternalId = Guid.NewGuid()
            },
            new EventEntity()
            {
                TypeEventId=type_event.Id,
                Name="Кармен",
                Desctiption="Потрясающая опера, исполняемая артистами Московского Большого театра",
                AgeViewer=12,
                ExternalId = Guid.NewGuid()
            },
                };
                context.Events.AddRange(events);
                context.SaveChanges();

                //execute
                var repository = new Repository<EventEntity>(DbContextFactory);
                var actualEvent = repository.GetById(events[0].Id);

                //assert
                actualEvent.Should().BeEquivalentTo(events[0], options => options.Excluding(x => x.EventType));
            }
            [Test]
            public void GetByIdTest_NegativeCase()
            {
                //prepare
                using var context = DbContextFactory.CreateDbContext();
                var type_event = new EventType()
                {
                    Name = "Опера",
                    ExternalId = Guid.NewGuid()
                };
                context.TypesEvents.Add(type_event);
                context.SaveChanges();

                var events = new EventEntity[]
                {
            new EventEntity()
            {
                TypeEventId=type_event.Id,
                Name="Руслан и Людмила",
                Desctiption="Потрясающая опера, исполняемая артистами Московского Большого театра",
                AgeViewer=6,
                ExternalId = Guid.NewGuid()
            },
            new EventEntity()
            {
                TypeEventId=type_event.Id,
                Name="Кармен",
                Desctiption="Потрясающая опера, исполняемая артистами Московского Большого театра",
                AgeViewer=12,
                ExternalId = Guid.NewGuid()
            },
                };
                context.Events.AddRange(events);
                context.SaveChanges();

                //execute
                var repository = new Repository<EventEntity>(DbContextFactory);
                var actualEvent = repository.GetById(events[events.Length - 1].Id + 1);

                //assert
                actualEvent.Should().BeNull();
            }

            [SetUp]
            public void SetUp()
            {
                CleanUp();
            }

            [TearDown]
            public void TearDown()
            {
                CleanUp();
            }

            public void CleanUp()
            {
                using (var context = DbContextFactory.CreateDbContext())
                {
                    context.Events.RemoveRange(context.Events);
                    context.TypesEvents.RemoveRange(context.TypesEvents);
                    context.SaveChanges();
                }
            }
        }
    }
}
