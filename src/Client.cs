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

                        Type t = Type.GetType("Trifolium.Events");
                        MethodInfo mi = t.GetMethod(jsonobj.e);
                        mi.Invoke(t, new object[] { socket, jsonobj.d });
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