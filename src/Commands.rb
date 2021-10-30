

module NeoRPGServer
    module Command

        def Login(credentials)
            return false if credentials == nil
            login_data = NeoRPGServer::SQL::GetData("*", "users", credentials.uname)
            return true
        end
    end
end