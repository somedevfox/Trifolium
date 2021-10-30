
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
            loop {
                conn = @server_sock.accept
                Thread.new(conn) { |server_sock| 
                    NeoRPGServer::Logger.LogSuccess(NeoRPGServer::Strings::PREFIX_Server, NeoRPGServer::Strings::STATUS_NewConnection);
                    
                    # Creating new CLIENT for CONNECTION
                    client = $ClientManager.Add(conn)
    
                    begin
                        messageBuffer = ''
                        while client.connected?
                            messageBuffer += conn.recv(0xFFFF)
                            
                            case messageBuffer
                                when /\ANRMSINIT\\(.+)\\(.+)\\(.+)/ 
                                    client.Decline("ERR\\invalid_libver") if $1 != NRPGS_VERSION
                                    client.Decline("ERR\\invalid_gamever") if $1 != @configfile["Server Info"]["GameVersion"]
                                    break
                                when /\AL\\(.+)\\(.+)/

                                    break
                            end
                        end
                    end
                }
            }
        end
    end
end