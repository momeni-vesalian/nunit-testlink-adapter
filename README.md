# nunit-testlink-adapter
NUnit to TestLink adapter by Ali Asghar Momeni Vesalian, Alireza Nikooee and Sajjad Mokari. This repository only contains projects for NUnit and TestLink.

NuGet: The TestLinkFixture and NUnitTestLinkAddOn are available on nuget http://www.nuget.org/packages?q=testlink and XML-RPC.NET-Client 3.0.0.266 is available on nuget http://www.nuget.org/packages?q=xmlrpcnet

Dependencies: NUnit 2.6.4 .NET 4.0 TestLink [DEV] 1.9.14

Build: I use VS 2015 and NuGet (dependencies to XmlRpcV2, NUnit and NUnit.Runners). I run all tests of TestLinkAdapter.Test.

News: All TestLinkFixture attributes will be declared automatically in testlink but for the first runnig of tests all tests will be failed because you must create your platform manually and assign it to the test plan; After that you can run all tests again and all of them will be passed successfully. 
