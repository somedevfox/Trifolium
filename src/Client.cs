using System;
using System.IO;
using System.Net;
using System.Text;
using System.Reflection;
using System.Net.Sockets;
using System.Net.Security;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

using Utf8Json;
using Trifolium;

namespace Trifolium
{
    class Client
    {
        X509Certificate serverCert;
        bool ClientInitialized = false;
        public Client(TcpClient tcpcl)
        {
            try
            {
                // [ANCHOR] | Import existing certificate OR create new one.
                Logger.Log("Client Thread", "Importing certificate...");
                if(!Directory.Exists("cert"))
                    Directory.CreateDirectory("cert");
                if (!File.Exists("cert/server.cert") || !File.Exists("cert/server.pfx"))
                {
                    var ecdsa = ECDsa.Create(); // * Generate asymmetric key pair.
                    var req = new CertificateRequest("cn=technologies.somedevfox.com",
                                                     ecdsa,
                                                     HashAlgorithmName.SHA256); // * Create certificate request.
                    var cert = req.CreateSelfSigned(DateTimeOffset.Now,
                                                    DateTimeOffset.Now.AddYears(10)); // * And create self-signed certificate using request we've created before.

                    // * Now, we only need to export public certificate and private key.
                    File.WriteAllText("cert/server.pfx",
                    "-----BEGIN RSA PRIVATE KEY-----\r\n"
                    + Convert.ToBase64String(cert.Export(X509ContentType.Pkcs12), Base64FormattingOptions.InsertLineBreaks)
                    + "\r\n-----END RSA PRIVATE KEY-----"); // * Private key.
                    File.WriteAllText("cert/server.cert",
                    "-----BEGIN CERTIFICATE-----\r\n"
                    + Convert.ToBase64String(cert.Export(X509ContentType.Cert), Base64FormattingOptions.InsertLineBreaks) +
                    "\r\n-----END CERTIFICATE-----"); // * Public certificate.
                }
                serverCert = X509Certificate.CreateFromCertFile("cert/server.cert");
                Logger.LogSuccess("Client Thread", "Loaded! Creating Secure Sockets Layer stream...");

                /*SslStream sslstream = new SslStream(tcpcl.GetStream(), false);
                sslstream.AuthenticateAsServer(serverCert,
                                               clientCertificateRequired: false,
                                               checkCertificateRevocation: true);*/
                Logger.LogSuccess("Client Thread", "Created! Listening for messages.");
                Socket socket = tcpcl.Client;

                byte[] bytes = new byte[1024]; // * Buffer (bytes).

                // [ANCHOR] | Listen for messages from client.
                while (true)
                {
                    // * Check if socket have sended a message.
                    // * Why: JObject will complain that it cannot parse empty messages.
                    if (socket.Available > 0)
                    {
                        // [ANCHOR] | Make buffer human-readable.
                        int numByte = socket.Receive(bytes);
                        string data = null;
                        data += Encoding.UTF8.GetString(bytes, 0, numByte); // * Buffer (string).

                        Structs.Error err = new Structs.Error();

                        // [ANCHOR] |  Parse JSON buffer.
                        var jsonobj = Utf8Json.JsonSerializer.Deserialize<Structs.Event>(data);
                        /*if (jsonobj.e == "test") // * Test event.
                            Logger.Log("Client Thread", "This connection was created by client to test server's availability.");
                        else if (jsonobj.e == "init")
                        { // * Event for checking library, game versions and extensions between client and server.
                            Logger.Log("Client Thread", "Real connection was established by client.");

                            Structs.Status status = new Structs.Status(); // * Structs are required by JsonConvert.

                            status.e = "init"; // * Set status event.
                            if (jsonobj.d["libver"] != Info.libver)
                            {
                                Logger.LogError("Client Thread", "Client-side library is outdated, disconnecting and terminating thread.");
                                status.sid = 20; // * ERR: Trifolium is outdated.
                                socket.Close();
                                return;
                            }
                            else if (jsonobj.d["gamever"] != Info.gamever)
                            {
                                Logger.LogError("Client Thread", "Game version is outdated, disconnecting and terminating thread.");
                                status.sid = 21; // * ERR: Game is outdated.
                                return;
                            }
                            else if (Info.GetExtensions(jsonobj.d["extensions"]) != Info.extensions.ToString())
                            {
                                Logger.LogError("Client Thread", "Client and server extensions do not match, disconnecting and terminating thread.");
                                status.sid = 22; // * ERR: Extension lists do not match.
                                return;
                            }
                            else
                                status.sid = 10; // * Everything is fine :) Connection success.
                            
                            socket.Send(
                                        JsonSerializer.Serialize<Structs.Status>(status)
                                    ); // * Aaaand send result to client (we need to turn buffer string to bytes first).
                            if (status.sid.ToString().StartsWith("2")) // * If StatusId starts with error prefix, close connection.
                                socket.Close();
                            ClientInitialized = true; // * Buf if StatusId is NOT 2 - set that this client is valid.
                            
                            Events events = new Events();
                        }
                        else if (jsonobj.e == "login")
                        { // * Login event.
                            Logger.Log("Client Thread", $"{jsonobj.d["username"]} attempts to log in.");
                        }*/
                        Dictionary <string, Delegate> EventMethods = new Dictionary<string, Delegate>();
                        foreach (MethodInfo m in typeof(Events).GetMethods())
                        {
                            EventMethods.Add(m.Name, m.CreateDelegate<Delegate>());
                        }
                        if(EventMethods.ContainsKey(jsonobj.e)) {
                            Logger.Log("Client Thread Debug", "contains key");
                            EventMethods[jsonobj.e].DynamicInvoke(socket, jsonobj.d);
                        } else {
                            Logger.LogError("Client Thread", "Client tried to invoke an unexistent event.");
                            err.id = 10;
                            err.message = "invalid_event";
                            socket.Send(Encoding.UTF8.GetBytes(JsonSerializer.Serialize<Structs.Error>(err).ToString()));
                        }
                    }

                }
            }
            catch(JsonParsingException jsonErr) {
                Logger.LogError("Client Thread", "Client have sent invalid JSON payload.");
                return;
            }
            catch (SocketException socketErr)
            {
                Logger.LogError("Client Thread", "Socket was unexpectedly terminated. Message: " + socketErr.Message);
                return;
            }
            catch(ArgumentNullException ex) {
                Logger.LogError("Client Thread", "Stopping execution due to one of arguments being null. Message: " + ex.Message);
                return;
            }
            catch (Exception ex)
            {
                Logger.LogError("Client Thread", "Unexpected exception. Message: " + ex.Message);
            }

        }

        public string ReadSecureMessage()
        {
            throw new NotImplementedException();
        }
    }
}