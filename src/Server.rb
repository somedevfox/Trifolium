
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
        end
    end
end