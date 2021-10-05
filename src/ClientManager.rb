#------------------------------------------------#
# NeoRPGServer::ClientManager
#
# As name suggests, this script manages CLIENTS.
#------------------------------------------------#

module NeoRPGServer
    class ClientManager
        def initialize 
            @mutex = Mutex.new
            @client_list = [];
        end
        
        #------------------------------------------------#
        # Adds new CLIENT.
        #  client - Socket connection.
        # RETURNS: Client Class.
        #------------------------------------------------#
        def Add(client)
            @client_list.push(client)
            return Client.new(client)
        end

        def Remove(client)
            @client_list[client] = nil;
        end
    end
end