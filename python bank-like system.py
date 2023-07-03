#OON ZHENG KAI
#TP064390

import datetime
#default bank interface
def interface():
    print("Welcome to RICH bank!")
    print("to log in type (login)")
    print("to register type (register)")

#create admin function
def create_admin(AID,AP):
    admin_file = open("admin_list.txt","a")
    id_and_password = (AID,AP)
    admin = "\t".join(id_and_password)
    admin_file.write(admin + "\n")
    admin_file.close()
    print("create done.")

#admin interface
def admin_interface(adminID):
    #display the current admin
    print("log in ID :",adminID)
    print("1.Create New Customer Acc")
    print("2.Modify Customer Detail")
    print("3.Read Customer Detail File")
    print("4.Change your admin password")
    print("5.Print Customer's Statement of Account Report")
    print("6.Quit")
    option =  input("choose your option:")
    return option

#for admin modify customer detail
def Modify_Customer_Detail():
    validation = input("Enter the user's name:")
    with open("customer_register.txt","r") as file_handle:
        for user in file_handle:
            if user.startswith(validation):
                o = user.split("\t")
                with open('customer_register.txt', 'r') as modify :
                    change_detail = modify.read()
                while True:
                    old = input("Enter the old detail:")
                    #for limit the admin cannot change user's name, IC and userID
                    if old == o[2] or old == o[3]:
                        new = input("Enter the new detail:")
                        change_detail = change_detail.replace(old, new)
                        break
                    else:
                        print("User's name, IC and useID cannot be change or invalid detail!")
                        continue
                with open('customer_register.txt', 'w') as modify:
                    modify.write(change_detail)
            else:
                #pass for avoid crash when the user is not in the first line
                pass

#for admin to check the customer detail
def Read_Customer_Detail_File():
    #read the available customer
    register_file = open("customer_register.txt","r")
    print(register_file.read())

#for admin to change his own password
def Change_admin_password():
    #change password
    with open('admin_list.txt', 'r') as Afile :
        change_password = Afile.read()
    change_password = change_password.replace(password, input("enter your new password:"))
    with open('admin_list.txt', 'w') as Afile:
         Afile.write(change_password)

#auto generate savingID and password
def saving_customer():     
    #create savingID
    S_count = 1
    saving_Fhandle = open("customer_account.txt")
    for S_number in saving_Fhandle:
        S_count = S_count + 1
    Sback_code = str(S_count)
    saving_Fhandle.close()
    savingID = "saving" + Sback_code.rjust(4,"0")
    #create password
    SDP = datetime.datetime.now()
    default_password = (SDP.strftime("%f"))
    #combine and store the new saving account
    saving_file = open("customer_account.txt","a")
    S_ID_password = (savingID,default_password)
    saving = "\t".join(S_ID_password)
    saving_file.write(saving + "\n")
    saving_file.close()
    #for the admin to enter the third argument of the add_id
    print("new saving ID is:",savingID)
    print("create done")

#auto generate currentID and password
def current_customer(): 
    #create currentID
    C_count = 1
    current_Fhandle = open("customer_account.txt")
    for C_number in current_Fhandle:
        C_count = C_count + 1
    Cback_code = str(C_count)
    current_Fhandle.close()
    currentID = "current" + Cback_code.rjust(4,"0")
    #create password
    CDP = datetime.datetime.now()
    C_default_password = (CDP.strftime("%f"))
    #combine and store the new current account
    current_file = open("customer_account.txt","a")
    C_ID_password = (currentID,C_default_password)
    current = "\t".join(C_ID_password)
    current_file.write(current + "\n")
    current_file.close()
    #for the admin to enter the third argument of the add_id
    print("new current ID is:",currentID)
    print("create done")

#add new account ID in the customer detail and add the first deposit in the file
def add_id(N,A,NID):
    RD = datetime.datetime.now()
    #set the real date
    Rtime = (RD.strftime("%x"))
    f = open("customer_register.txt")
    lines = f.readlines()
    for detail in lines:
        b = detail.split("\t")
        #search the specific customer detail and replace his/her deposit to userID
        if N in b[0]:
            with open('customer_register.txt', 'r') as file :
                filedata = file.read()
            filedata = filedata.replace(b[5], NID + "\n")
            with open('customer_register.txt', 'w') as file:
                file.write(filedata)
    f.close()
    #add first deposit in the transaction file
    with open ("customer_transaction_list.txt","a") as deposit:
        deposit.write(NID + "\t" + Rtime + "\t" + "+" + "\t" + A + "\n")
    #store customer amount
    with open("customer_amount.txt","a") as record_amount:
        record_amount.write(NID + "\t" + A + "\n")   
    print("detail changed")

#customer interface 
def customer_interface(customerid):
    print("log in ID:",customerid)
    print("1.Deposit")
    print("2.Withdrawn")
    print("3.Print Customer's Statement of Account Report")
    print("4.Check Current Amount")
    print("5.Change Password")
    print("6.Quit")
    option = input("choose your option:")
    return option

#deposit function
def deposit(ID):
    T = datetime.datetime.now()
    #for record the date of transaction
    date = (T.strftime("%x"))
    with open("customer_amount.txt","r") as file_handle:
        for user in file_handle:
            #read the specified user detail
            if user.startswith(ID):
                y = user.split("\t")
                #count the new amount
                old_amount = int(y[1])
                deposit = int(input("Enter your deposit amount:"))
                new_amount = old_amount + deposit
                print("your current amount is RM",new_amount)
                #read the file to take the detail
                with open('customer_amount.txt', 'r') as file :
                    filedata = file.read()
                #change the total amount
                filedata = filedata.replace(y[1],str(new_amount) + "\n")
                #rewrite the whole file into the file
                with open('customer_amount.txt', 'w') as file:
                    file.write(filedata)
                #record the date of transaction
                with open("customer_transaction_list.txt","a") as history:
                    history.write(ID + "\t" + date + "\t" + "+" + "\t" + str(deposit) + "\n")

#withdrawn function
def withdrawn(ID):
    x = datetime.datetime.now()
    #for record the date of transaction
    date = (x.strftime("%x"))
    with open("customer_amount.txt","r") as file_handle:
        for user in file_handle:
            if user.startswith(ID):
                y = user.split("\t")
                #count the new amount
                while True:
                    old_amount = int(y[1])
                    withdrawn = int(input("Enter your withdrawn amount:"))
                    new_amount = old_amount - withdrawn
                    #make sure the saving amount will not lower than RM200
                    if ID.startswith("saving") and new_amount < 200:
                        print("your saving account cannot lower than RM200")
                        continue
                    #make sure the current amount will not lower than RM500
                    elif ID.startswith("current") and new_amount < 500:
                        print("your current acccount cannot lower than RM500")
                        continue
                    else:
                        print("your current is RM",new_amount)
                        #read the file to take the detail
                        with open('customer_amount.txt', 'r') as file :
                            filedata = file.read()
                        #change the total amount
                        filedata = filedata.replace(y[1],str(new_amount) + "\n")
                        #rewrite the whole file into the file
                        with open('customer_amount.txt', 'w') as file:
                            file.write(filedata)
                        #record the date of transaction
                        with open("customer_transaction_list.txt","a") as history:
                            history.write(ID + "\t" + date + "\t" + "-" + "\t" + str(withdrawn) + "\n")
                            break

#transaction history function
def transaction_history(ID):
    #choose to display the whole transaction or specified transaction
    print("1.Check Specific Transaction\n2.Check Whole Transaction")
    option_time = input("Enter your choice(1-2):")
    if option_time == "1":
        while True:
            #enter the start date
            year_start = str(input("Enter the start date of year(YYYY):"))
            if len(year_start) != 4:
                print("Please follow the instruction to key in the correct year")
                continue
            month_start = int(input("Enter the start date of month(1-12):"))
            if month_start not in range(1,13):
                print("Please follow the instruction to key in the month")
                continue
            day_start = int(input("Ente the start date of day(1-31):"))
            if day_start not in range(1,32):
                print("Please follow the instruction to key in the day")
                continue
            else:
                break
        specific_date_start = datetime.datetime(int(year_start),month_start,day_start)
        start = (specific_date_start.strftime("%x"))
            #enter the end date
        while True:
            year_end = str(input("Enter the end date of year(YYYY):"))
            if len(year_end) != 4:
                print("Please follow the instruction to key in the correct year")
                continue
            month_end = int(input("Enter the end date of month(1-12):"))
            if month_end not in range(1,13):
                print("Please follow the instruction to key in the month")
                continue
            day_end = int(input("Enter the end date of day(1-31):"))
            if day_end not in range(1,32):
                print("Please follow the instruction to key in the day")
                continue
            else:
                break
        specific_date_end = datetime.datetime(int(year_end),month_end,day_end)
        end = (specific_date_end.strftime("%x"))
        #display the specified duration transaction history
        with open("customer_transaction_list.txt","r") as file:
            #format
            print("\nCustomer's Statement of Account Report\n")
            for history in file:
                if history.startswith(ID):
                    b = history.split("\t")
                    if b[1] >= start and b[1] <= end:
                        print(b[1] + "\t" + b[2] + b[3])
                    else:
                        #pass for ignore the duration that doesn't select by user
                        pass
    elif option_time == "2":
        #display whole transaction history
        with open("customer_transaction_list.txt","r")as f:
            #format
            print("\nCustomer's Statement of Account Report\n")
            for history in f:
                if history.startswith(ID):
                    a = history.split("\t")
                    print("date:",a[1],"transaction:",a[2] + a[3])
                else:
                    #pass for ignore the history that not the current user
                    pass
    else:
        print("invalid option")

#check current amount function
def current_amount(ID):
    with open("customer_amount.txt","r")as file:
        for detail in file:
            if detail.startswith(ID):
                seperate = detail.split("\t")
                print("your current amount is RM",seperate[1])

#for customer change his own password
def Change_customer_Password():
    with open('customer_account.txt', 'r') as customer_file :
        change_password = customer_file.read()
    new_password = input("Enter your new password:")
    change_password = change_password.replace(password, new_password)
    with open('customer_account.txt', 'w') as customer_file:
        customer_file.write(change_password)
    print("your new password is,",new_password)

#customer detail
def register_form():
    #user enter detail
    N = input("Enter your name:")
    IC = input("Enter your IC number:")
    PN = input("Enter your phone number:")
    HA = input("Enter your home address:")
    while True:
        AT = input("Type of Bank Account(Saving or Current):")
        #make sure the amount is not lower than the RM200 or RM500
        A = input("Deposit the amount (saving > RM200, current > 500):")
        x = int(A)
        if AT == "saving":
            if x < 200:
                print("saving account must deposit at least RM200")
                continue
            else:
                break
        elif AT == "current":
            if x < 500:
                print("current account must deposit at least RM500")
                continue
            else:
                break
        else:
            print("invalid option")
            continue
    #store the customer detail
    register_file = open("customer_register.txt","a")
    mix = (N,IC,PN,HA,AT,A)
    detail = "\t".join(mix)
    register_file.write(detail + "\n")
    register_file.close()
    print("Register Done")
    print("Thanks for choosing us")
    print("Please wait for the admin stuff to create a new account and message to you!")
    
#user interface
interface()
choice = input("Enter your choice:")
#to log in or register
if choice == "login":
    user_id = input("Enter your use ID:")
    password = input("Enter your password:")
    #key is for user to log in
    key = user_id + "\t" + password + "\n"
    #superuser login #one superuser only
    if user_id == "superuser" and password == "SUPER666user":
        #super user interface
        while True:
        #superuser fuction
            print("1.Create New Admin Account\n2.Read Available Admin\n3.Quit")
            choice = input("choose your option (1-3):")
            if choice == "1":
                #userID starts with admin for admin log in 
                print("New Admin account must start with 'admin'!")
                new_admin_id = input("Enter a new adminID:")
                if new_admin_id.startswith("admin"):
                    new_a_password = input("Enter new admin password:")
                    #store the new admin account
                    create_admin(new_admin_id,new_a_password)
                    continue
                else:
                    print("new admin account must start with 'admin'")
                    continue
            elif choice == "2":
                #check the available admin account
                read_admin = open("admin_list.txt","r")
                print(read_admin.read())
                continue
            elif choice == "3":
                break
            else:
                print("invalid option")
                continue
    # admin log in
    elif user_id.startswith("admin"):
            admin_file = open("admin_list.txt","r")
            #become list
            line = admin_file.readlines()
            #if key match with the list then can log in
            if key in line:
                while True:
                    option = admin_interface(user_id)
                    if option == "1":
                        acc_type = input("saving or current?")
                        if acc_type == "saving":
                            saving_name = input("Enter the user name:")
                            saving_amount = input("Enter the user deposit amount:")
                            saving_customer()
                            put_Sid = input("Enter the new savingID:")
                            #enter the new ID for store ID and first transaction
                            add_id(saving_name,saving_amount,put_Sid)
                            continue
                        elif acc_type == "current":
                            current_name = input("Enter the user name:")
                            current_amount = input("Enter the user deposit amount:")
                            current_customer()
                            put_Cid = input("Enter the new currentID:")
                            #enter the new ID for store ID and first transaction
                            add_id (current_name,current_amount,put_Cid)
                            continue
                        else:
                            print("invaild account type")
                            continue
                    elif option == "2":
                        Modify_Customer_Detail()
                        continue
                    elif option == "3":
                        Read_Customer_Detail_File()
                        continue
                    elif option == "4":
                        Change_admin_password()
                        continue
                    elif option == "5":
                        #check the specified customer transaction
                        customer = input("Enter the userID:")
                        transaction_history(customer)
                        continue
                    elif option == "6":
                        break
                    else:
                        print("invalid option")
                        continue
            else:
                print("wrong userID or password")

    # saving cus log in
    elif user_id.startswith("saving"):
        saving_file = open("customer_account.txt","r")
        Sdetail = saving_file.readlines()
        while True:
            if key in Sdetail:
                S_option = customer_interface(user_id)
                if S_option == "1":
                    deposit(user_id)
                    continue
                elif S_option == "2":
                    withdrawn(user_id)
                    continue
                elif S_option == "3":
                    transaction_history(user_id)
                    continue
                elif S_option == "4":
                    current_amount(user_id)
                    continue
                elif S_option == "5":
                    Change_customer_Password()
                    continue
                elif S_option == "6":
                    break
                else:
                    print("invalid option")
                    continue
            else:
                print("wrong ID or password")
                break

    # current cus log in
    elif user_id.startswith("current"):
        current_file = open("customer_account.txt","r")
        Cdetail = current_file.readlines()
        while True:
            if key in Cdetail:
                C_option = customer_interface(user_id)
                if C_option == "1":
                    deposit(user_id)
                    continue
                elif C_option == "2":
                    withdrawn(user_id)
                    continue
                elif C_option == "3":
                    transaction_history(user_id)
                    continue
                elif C_option == "4":
                    current_amount(user_id)
                    continue
                elif C_option == "5":
                    Change_customer_Password()
                    continue
                elif C_option == "6":
                    break
                else:
                    print("invalid option")
                    continue
            else:
                print("invalid ID or password")
                break
    else:
    # if user acc had not registered
        print("Invalid userID")

# customer register form
elif choice == "register":
    register_form()
else:
    print("Invalid option")
print("Thanks for using RICH bank!")
