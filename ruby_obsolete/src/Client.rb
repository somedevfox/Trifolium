#------------------------------------------------#
# Client
#
# Client interface. (SERVER-SIDE)
#------------------------------------------------#

module NeoRPGServer
    class Client
        def initialize(socket)
            @authentication_mutex = Mutex.new
            @last_message = ''
            @socket = socket
        end

        def connected?
            return !@socket.closed?
        end

        def Decline(message)
            @socket.puts message
            @socket.close
        end
    end
end