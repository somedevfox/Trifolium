
module NeoRPGServer
    class Server
        attr_reader :sql
        attr_reader :server_sock
        def initialize(config)
            @running = true
            @sql = nil
            @config = config
            self.start
        end

        def start  
            # Connect to database.
            @sql = NeoRPGServer::SQL.new
            # Creates socket.
            @server_sock = TCPServer.new(@config[:Server]["Host"], @config[:Server]["Port"])
            self.AcceptConnections
        end

        def AcceptConnections
            NeoRPGServer::Logger.LogSuccess(NeoRPGServer::Strings::PREFIX_Server, NeoRPGServer::Strings::STATUS_AcceptingConnections)
            # Accepting connections
            while !@server_sock.closed?
                begin 
                    conn = @server_sock.accept
                rescue
                    retry
                end
            end


            # Creating thread for new connection
            Thread.new(conn) { |server_sock| 
                NeoRPGServer::Logger.LogSuccess(NeoRPGServer::Strings::PREFIX_Server, NeoRPGServer::Strings::STATUS_NewConnection);
                
                # Creating new CLIENT for CONNECTION
                client = @ClientManager.Add(conn)

                begin
                    messageBuffer = ''
                    while client.connected?
                        messageBuffer += conn.recv(0xFFFF)
                        
                        puts messageBuffer
                    end
                end
            }
        end
    end
end