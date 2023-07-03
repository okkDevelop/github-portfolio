#Oon Zheng Kai
#TP064390
#PFDA assignment

#load or install package
install.packages("magrittr")
install.packages("dplyr")
install.packages("fmsb")
install.packages("treemap")
install.packages("GGally")
library(ggplot2)
library(magrittr)
library(dplyr)
library(plotrix)
library(fmsb)
library(treemap)
library(GGally)

#read and set the data as valuable
read_data <- read.csv("D:/APU homework/Y2S1 - assignment/House_Rent_Dataset.csv")

#check the data has NA or not
apply(read_data,2,function(x) any(is.na(x)))

#call data with table
View(read_data)

#to know each of the data name in specific column
factor(read_data$City)
#Question 1 What is the trend that in renting
#Analysis 1 Analysis the number of houses in each area
{
  #to know the number of the specific city
  City1 <- nrow(read_data[read_data$City == "Bangalore",])
  City2 <- nrow(read_data[read_data$City == "Chennai",])
  City3 <- nrow(read_data[read_data$City == "Delhi",])
  City4 <- nrow(read_data[read_data$City == "Hyderabad",])
  City5 <- nrow(read_data[read_data$City == "Kolkata",])
  City6 <- nrow(read_data[read_data$City == "Mumbai",])
  #add all the argument for calculate the percentage
  totalOfCity <- c(City1,City2,City3,City4,City5,City6)
  #create a percentage valuable
  percentOfCity <- paste0(round(100*totalOfCity/sum(totalOfCity)),"%")
  percentOfCity
  combineCity <- c("Bangalore","Chennai","Delhi","Hyderabad","Kolkata","Mumbai")
  #to make the data as pie
  pie(totalOfCity, percentOfCity, radius = 0.65, main = "Percentage of house rent in each city",col = c("red","orange","yellow","green","blue","blue4"))
  #add legend in the pie chart
  legend("bottom", combineCity, cex = 0.6, fill = c("red","orange","yellow","green","blue","blue4"),horiz = TRUE)
}

#Analysis 2 Analysis the number of different furnished status
#to know each data name in the specific column
factor(read_data$Furnishing.Status)
{
  furnished <- nrow(read_data[read_data$Furnishing.Status == "Furnished",])
  semi_furnished <- nrow(read_data[read_data$Furnishing.Status == "Semi-Furnished",])
  unfurnished <- nrow(read_data[read_data$Furnishing.Status == "Unfurnished",])
  #add all the argument for calculate the percentage
  totalOfStatus <- c(furnished, semi_furnished, unfurnished)
  #create a percentage valuable
  percentOfStatus <- paste0(round(100*totalOfStatus/sum(totalOfStatus)),"%")
  combineStatus <- c("Furnished","Semi-Furnished","Unfurnished")
  #to make the data as pie
  pie(totalOfStatus, percentOfStatus, radius = 0.65, main = "Percentage of furnishing status",col = c("red","orange","yellow"))
  #add legend in the pie chart
  legend("bottom", combineStatus, cex = 0.6, fill = c("red","orange","yellow"),horiz = TRUE)
}

#Analysis 3 Analysis the number of tenant preferred
#to know each data name in the specific column
factor(read_data$Tenant.Preferred)
{
  tenant_bachelors <- nrow(read_data[read_data$Tenant.Preferred == "Bachelors",])
  tenant_both <- nrow(read_data[read_data$Tenant.Preferred == "Bachelors/Family",])
  tenant_family <- nrow(read_data[read_data$Tenant.Preferred == "Family",])
  #add all the argument for calculate the percentage
  totalPreferred <- c(tenant_bachelors,tenant_both,tenant_family)
  #create a percentage valuable
  percentOfPreferred <- paste0(round(100*totalPreferred/sum(totalPreferred)),"%")
  combinePreferred <- c("Bachelors","Bachelors/Family","Family")
  #to make the data as pie
  pie(totalPreferred, percentOfPreferred, radius = 0.65, main = "Percentage of tenant Preferred",col = c("red","orange","yellow"))
  #add legend in the pie chart
  legend("bottom", combinePreferred, cex = 0.6, fill = c("red","orange","yellow"),horiz = TRUE)
}

#Analysis 4 Analysis the range of floor that in rent
{
  #keep and put the data before "out of" in specific column into new data set
  read_data$floorLevel = as.character(sub(" out of .*$", "", read_data$Floor))
  #replace the character into convertible character
  read_data$floorLevel = replace(read_data$floorLevel,read_data$floorLevel == "Ground","0")
  read_data$floorLevel = replace(read_data$floorLevel,read_data$floorLevel == "Upper Basement","-1")
  read_data$floorLevel = replace(read_data$floorLevel,read_data$floorLevel == "Lower Basement","-2")
  converter <- as.integer(read_data$floorLevel)
  indexground <- 0L
  indexLowLevel <- 0L
  indexMidLevel <- 0L
  indexHighLevel <- 0L
  converter <- as.integer(read_data$floorLevel)
  for(calculate in converter)
  {
    if(calculate <= 0)
    {
      indexground = indexground + 1
    }
    else if(calculate > 0 & calculate <= 10)
    {
      indexLowLevel = indexLowLevel + 1
    }
    else if(calculate > 10 & calculate <= 19)
    {
      indexMidLevel = indexMidLevel + 1
    }
    else
    {
      indexHighLevel = indexHighLevel + 1
    }
  }
  combineFloor <- c(indexground,indexLowLevel,indexMidLevel,indexHighLevel)
  percentOfFloor <- paste0(round(100*combineFloor/sum(combineFloor),2),"%")
  dataOfFloor <- c("ground or basement","lower than 10","lower than 20","higher than 20")
  pie(combineFloor, percentOfFloor, cex = 0.6, radius = 0.8, main = "Percentage of the floor range", col = c("red","orange","yellow","green"))
  legend("bottom", dataOfFloor, cex = 0.55, fill = c("red","orange","yellow","green"),horiz = TRUE)
}

#Analysis 5 Analysis the number of different point of contact
#to know each data name in the specific column
factor(read_data$Point.of.Contact)
{
  contact_agent <- nrow(read_data[read_data$Point.of.Contact == "Contact Agent",])
  contact_builder <- nrow(read_data[read_data$Point.of.Contact == "Contact Builder",])
  contact_owner <- nrow(read_data[read_data$Point.of.Contact == "Contact Owner",])
  #add all the argument for calculate the percentage
  totalContact <- c(contact_agent,contact_builder,contact_owner)
  #create a percentage valuable
  percentOfContact <- paste0(round(100*totalContact/sum(totalContact),2),"%")
  combineContact <- c("Contact Agent","Contact Builder","Contact Owner")
  #to make the data as pie
  pie(totalContact, percentOfContact, radius = 0.65, main = "Percentage of the contact", col = c("red","orange","yellow"))
  #add legend in the pie chart
  legend("bottom", combineContact, cex = 0.6, fill = c("red","orange","yellow"),horiz = TRUE)
}

#Analysis 6 Analysis what type of houses are in trend
{
  #replace the specific column and store it into new data frame
  read_data$Highest_Level = as.integer(sub("^.* out of ", "", read_data$Floor))
  #calculate the house
  numberOfHouse = read_data %>% filter(read_data$Highest_Level < 3)
  #calculate the condo
  numberOfcondo = read_data %>% filter(read_data$Highest_Level >= 3)
  totalOfHouses <- c(nrow(numberOfHouse),nrow(numberOfcondo))
  pie(totalOfHouses, totalOfHouses, radius = 0.6, main = "Number of different type of houses", col = c("red","orange"))
  legend("bottom", c("house","condo"), cex = 0.7, fill = c("red","orange"),horiz = TRUE)
}

#Analysis 7 Analysis the number of posts in specific month
{
  #define valuable to calculate the number of specific month
  indexApril <- 0L
  indexMay <- 0L
  indexJune <- 0L
  indexJuly <- 0L
  #replace the specific month to specific number
  read_data$Posted_April = as.character(sub("4.*$","4",read_data$Posted.On))
  read_data$Posted_May = as.character(sub("5.*$","5",read_data$Posted.On))
  read_data$Posted_June = as.character(sub("6.*$","6",read_data$Posted.On))
  read_data$Posted_July = as.character(sub("7.*$","7",read_data$Posted.On))
  #calculate the valuable of specific month
  for(dataApril in read_data$Posted_April)
  {
    if(dataApril == "4")
    {
      indexApril = indexApril +1
    }
  }
  for(dataMay in read_data$Posted_May)
  {
    if(dataMay == "5")
    {
      indexMay = indexMay +1
    }
  }
  for(dataJune in read_data$Posted_June)
  {
    if(dataJune == "6")
    {
      indexJune = indexJune + 1
    }
  }
  for(dataJuly in read_data$Posted_July)
  {
    if(dataJuly == "7")
    {
      indexJuly = indexJuly + 1
    }
  }
  #combine the valuable into a new valuable
  totalOfPostOn <- c(indexApril,indexMay,indexJune,indexJuly)
  #convert the data as pie chart
  pie(totalOfPostOn, totalOfPostOn , radius = 0.65, main = "Number of the post in each month", col = c("red","orange","yellow","green"))
  legend("bottom", c("April","May","June","July"), cex = 0.7, fill = c("red","orange","yellow","green"),horiz = TRUE)
}

#--------------------------------------------------------------------------------------
#Question 2 What affect the rent fee
#Analysis 1 Analysis relationship of rent fee and house size
rentSize = read_data %>% summarise(Rent,Size) %>% filter(Rent <= 1200000)
ggplot(rentSize, aes(x=Size, y=Rent)) + geom_point(aes(color=Rent)) +
  scale_y_continuous(breaks=c(100000, 200000, 300000, 400000, 500000, 600000, 700000, 800000, 900000, 1000000, 1100000, 1200000), 
                     labels=c('100000', '200000', '300000','400000','500000','600000','700000','800000','900000', '1000000', '1100000', '1200000'))+
  labs(title="Relation of Rent Fee and House Size")

#Analysis 2 Analysis relationship of rent fee and point of contact
Contact = read_data %>% group_by(Point.of.Contact) %>% filter(Rent<=200000) %>% 
  summarise(AverageRentalFee=mean(Rent, na.rm = TRUE))

ggplot(Contact, aes(x = Point.of.Contact, y=AverageRentalFee, fill = AverageRentalFee)) + 
  geom_bar(stat = "identity") + labs(title="Relationship of Rent Fee and Point of Contact") + 
  scale_y_continuous(breaks=c(5000,10000,20000,30000,40000,50000))

#Analysis 3 Analysis relationship of rent fee and area
city = read_data %>% group_by(City) %>% filter(Rent<=200000) %>% 
  summarise(AverageRentalFee=mean(Rent, na.rm = TRUE))

ggplot(city, aes(yend=0,y=AverageRentalFee,x=City,xend=City)) + 
  geom_point() + 
  geom_segment(aes(color=City,))+ scale_y_continuous(breaks=c(5000,10000,20000,30000,40000,50000,60000))+
  labs(title="Relationship of Rent Fee and City")

#Analysis 4 Analysis relationship of rent fee and furnish status
furnitureStatus = read_data %>% group_by(Furnishing.Status) %>% filter(Rent<=200000) %>% 
  summarise(AverageRentalFee=mean(Rent, na.rm = TRUE))

ggplot(furnitureStatus, aes(y=AverageRentalFee,x=Furnishing.Status,fill=Furnishing.Status)) + 
  geom_col()+ scale_y_continuous(breaks=c(5000,10000,20000,30000,40000,50000,60000))+
  labs(title="Relationship of Rent Fee and Furnishing Status")

#Analysis 5 Analysis relationship of rent fee and number of bathrooms
numberOfBathroom = read_data %>% group_by(Bathroom,Furnishing.Status) %>% filter(Rent<=200000 & Size<=3000)%>% 
  summarise(AverageRentalFee = mean(Rent, na.rm = TRUE))

ggplot(numberOfBathroom, aes(y = AverageRentalFee,x = Bathroom,fill = Furnishing.Status)) + geom_col(position="dodge") +
  labs(title="Relationship of Rent Fee and Number of Bathroom in different Furnish Status")


#Analysis 6 Analysis relationship of rent fee and number of BHK
numberOfBHK = read_data %>% group_by(BHK,Furnishing.Status) %>% filter(Rent<=200000 & Size<=3000)%>% 
  summarise(AverageRentalFee = mean(Rent, na.rm = TRUE))

ggplot(numberOfBHK, aes(y = AverageRentalFee,x = BHK,fill = Furnishing.Status)) + geom_col(position="dodge") +
  labs(title="Relationship of Rent Fee and BHK in different Furnish Status")

#Analysis 7 Analysis relationship of rent fee and date of the post
numApril = read_data %>% filter(substr(Posted.On,1,1) == "4") %>% 
  summarise(Month="April",AverageRentalFee=mean(Rent, na.rm = TRUE))
numMay = read_data %>% filter(substr(Posted.On,1,1) == "5") %>% 
  summarise(Month="May",AverageRentalFee=mean(Rent, na.rm = TRUE))
numJune = read_data %>% filter(substr(Posted.On,1,1) == "6") %>% 
  summarise(Month="June",AverageRentalFee=mean(Rent, na.rm = TRUE))
numJuly = read_data %>% filter(substr(Posted.On,1,1) == "7") %>% 
  summarise(Month="July",AverageRentalFee=mean(Rent, na.rm = TRUE))

combineMonth = rbind(numApril,numMay,numJune,numJuly)

ggplot(allmonth, aes(x=Month, y=AverageRentalFee)) + geom_line(color="red") + geom_point(aes(color=Month,shape = Month)) + 
  labs(title="Relationship of Rent Fee and Month") + scale_x_discrete(limits=unique(combineMonth$Month))

#Analysis 8 Analysis relationship of rent fee and the floor of houses
#keep and put the data before "out of" in specific column into new data set
read_data$floorLevel = as.character(sub(" out of .*$", "", read_data$Floor))
#replace the character into convertible character
read_data$floorLevel = replace(read_data$floorLevel,read_data$floorLevel == "Ground","0")
read_data$floorLevel = replace(read_data$floorLevel,read_data$floorLevel == "Upper Basement","-1")
read_data$floorLevel = replace(read_data$floorLevel,read_data$floorLevel == "Lower Basement","-2")

#convert the character as integer
rangeOfFloor <- as.integer(read_data$floorLevel)
rent <- read_data$Rent
combiner <- data.frame(rent,rangeOfFloor)
#filter the not usable data
fil <- combiner %>% filter(rent < 3500000 & rangeOfFloor < 76) %>% summarise(rent,rangeOfFloor)

ggplot(fil, aes(x = rangeOfFloor, y=rent)) + geom_point(aes(color = rent)) +
  scale_y_continuous(breaks=c(100000, 200000, 300000, 400000, 500000, 600000, 700000, 800000, 900000, 1000000, 1100000, 1200000), 
                     labels=c('100000', '200000', '300000','400000','500000','600000','700000','800000','900000', '1000000', '1100000', '1200000'))+
  labs(title="The Range Price of Each Level")

#Analysis 9 Analysis relationship of rent fee and tenant preferred
Preffered = read_data %>% group_by(Tenant.Preferred) %>% filter(Rent <= 200000) %>%
  summarise(AverageRentalFee = mean(Rent, na.rm = TRUE))
            
ggplot(Preffered, aes(x = Tenant.Preferred, y = AverageRentalFee, fill = AverageRentalFee)) +
  geom_bar(stat = "identity") + labs(title = "Relationship Of Rent Fee and Tenant Preffered") +
  scale_y_continuous(breaks = c(5000,10000,20000,30000,40000,50000))

#--------------------------------------------------------------------------------------
#Question 3 How much rent fee that affected by the furnishing status in different area type
#Analysis 1 Analysis the average rent fee of unfurnished status in different area type
unfurnishedArea = read_data %>% group_by(Area.Type) %>% 
  filter(Rent<=200000 & Size<=3000 & Furnishing.Status=="Unfurnished") %>% summarise(AverageRentalFee=mean(Rent, na.rm = TRUE))
ggplot(unfurnishedArea, aes(x=Area.Type, y=AverageRentalFee,fill=Area.Type)) +
  geom_col(size=1) +
  ggtitle("Relationship of Area Type and Rental Fee in Unfurnished Status")


#Analysis 2 Analysis the average rent fee of semi-furnished status in different area type
semiFunishedArea = read_data %>% group_by(Area.Type) %>% 
  filter(Rent<=200000 & Size<=3000 & Furnishing.Status=="Semi-Furnished") %>% summarise(AverageRentalFee=mean(Rent, na.rm = TRUE))
ggplot(semiFunishedArea, aes(x=Area.Type, y=AverageRentalFee,fill=Area.Type)) + geom_col(size=1) +
  ggtitle("Relationship of Area Type and Rental Fee in Semi-furnished Status")


#Analysis 3 Analysis the average rent fee of unfurnished status in different area type
furnishedArea = read_data %>% group_by(Area.Type) %>% 
  filter(Rent<=200000 & Size<=3000 & Furnishing.Status=="Furnished") %>% summarise(AverageRentalFee=mean(Rent, na.rm = TRUE))
ggplot(furnishedArea, aes(x=Area.Type, y=AverageRentalFee,fill=Area.Type)) +
  geom_col(size=1) +
  ggtitle("Relationship of Area Type and Rental Fee in Furnished Status")


#Analysis 4 Analysis the average rent fee of unfurnished status in different area type
unfurnishedArea = round(28557/13110*100,2)  
semiFunishedArea = round(39554/18322*100,2) 
furnishedArea = round(56174/24879*100,2) 
totalpercent = c(unfurnishedArea,semiFunishedArea,furnishedArea)
areaname = c("Unfurnished","Semi-Furnished","Furnished")
pie3D(totalpercent,labels=paste(totalpercent,"%"),explode=0.3,main="How Many Percent does Carpet Area Rental Fee Higher Than Super Area"
      ,theta=1,labelcex = 1)
legend("bottom", areaname, cex = 0.8, fill=c("red","blue","green"), horiz = TRUE)

#--------------------------------------------------------------------------------------
#Question 4 Which cities has the most expensive and cheapest rent fee in different furnishing status
#Analysis 1 Analysis the average cost of different area type in unfurnished in each city
unfurnished_type = read_data %>% group_by(Area.Type,City) %>% 
  filter(Furnishing.Status =="Unfurnished" & Area.Type != "Built Area") %>% 
  summarise(AverageRentalFee = mean(Rent, na.rm = TRUE))

ggplot(unfurnished_type, aes(yend = 0,y = AverageRentalFee,x = City,xend = City)) + 
  geom_point() + 
  geom_segment(aes(color = Area.Type))+ scale_y_continuous(breaks = c(5000,10000,20000,30000,40000,50000,60000))+
  labs(title = "Relationship Between Rent Fee and City in Unfurnished Status")

#Analysis 2 Analysis the average cost of different area type in semi-furnished in each city
semi_type = read_data %>% group_by(Area.Type,City) %>% 
  filter(Furnishing.Status =="Semi-Furnished" & Area.Type != "Built Area") %>% 
  summarise(AverageRentalFee = mean(Rent, na.rm = TRUE))

ggplot(semi_type, aes(yend = 0,y = AverageRentalFee,x = City,xend = City)) + 
  geom_point() + 
  geom_segment(aes(color=Area.Type)) + 
  labs(title = "Relationship Between Rent Fee and City in Semi-Furnished Status")

#Analysis 3 Analysis the average cost of different area type in furnished in each city
furnished_type = read_data %>% group_by(Area.Type,City) %>% 
  filter(Furnishing.Status == "Furnished" & Area.Type != "Built Area") %>% 
  summarise(AverageRentalFee = mean(Rent, na.rm = TRUE))

ggplot(furnished_type, aes(yend = 0,y = AverageRentalFee,x = City,xend = City)) + 
  geom_point() + 
  geom_segment(aes(color = Area.Type))+
  labs(title = "Relationship Between Rent Fee and City in Furnished Status")

#Analysis 4 Analysis the most expensive and cheapest rent with different furnishing status in each city
mostExpensive = read_data %>% filter(Rent > 2000000 & Area.Type != "Built Area") %>% summarise(City,Rent)

mostCheap = read_data %>% filter(Rent < 1300 & Area.Type != "Built Area") %>% summarise(City,Rent)

combine_most = rbind(mostExpensive,mostCheap)

ggplot(combine_most, aes(x = City, y = Rent, color = City,shape = City)) + geom_point(size=6) +
  scale_y_continuous(breaks = c(1200,150000,1000000,2000000,3000000,3500000),
                     labels = c("1200","150000","1000000","2000000","3000000","3500000")) +
  ggtitle("Which City Has The Most Expensive and Cheapest Rent")

#--------------------------------------------------------------------------------------
#Question 5 Which houses has the most value for money in each city
#Analysis 1 Analysis the lowest cost with biggest size of furnished house in super area in each city
#the biggest house size in Bangalore
comfort_Bangalore = read_data %>% filter(City == "Bangalore" & Area.Type == "Super Area" & Furnishing.Status == "Furnished")
#the price of the biggest house in Bangalore
comfortRent_Bangalore = comfort_Bangalore %>% filter(Size == max(comfort_Bangalore$Size)) %>% summarise(Rent)

#the biggest house size in Chennai
comfort_Chennai = read_data %>% filter(City == "Chennai" & Area.Type == "Super Area" & Furnishing.Status == "Furnished")
#the price of the biggest house in Chennai
comfortRent_Chennai = comfort_Chennai %>% filter(Size == max(comfort_Chennai$Size)) %>% summarise(Rent)

#the biggest house size in Delhi
comfort_Delhi = read_data %>% filter(City == "Delhi" & Area.Type == "Super Area" & Furnishing.Status == "Furnished")
#the price of the biggest house in Delhi
comfortRent_Delhi = comfort_Delhi %>% filter(Size == max(comfort_Delhi$Size)) %>% summarise(Rent)

#the biggest house size in Hyderabad
comfort_Hyderabad = read_data %>% filter(City == "Hyderabad" & Area.Type == "Super Area" & Furnishing.Status == "Furnished")
#the price of the biggest house in Hyderabad
comfortRent_Hyderabad = comfort_Hyderabad %>% filter(Size == max(comfort_Hyderabad$Size)) %>% summarise(Rent)

#the biggest house size in Kolkata
comfort_Kolkata = read_data %>% filter(City == "Kolkata" & Area.Type == "Super Area" & Furnishing.Status == "Furnished")
#the price of the biggest house in Kolkata
comfortRent_Kolkata = comfort_Kolkata %>% filter(Size == max(comfort_Kolkata$Size)) %>% summarise(Rent)

#the biggest house size in Mumbai
comfort_Mumbai = read_data %>% filter(City == "Mumbai" & Area.Type == "Super Area" & Furnishing.Status == "Furnished")
#the price of the biggest house in Mumbai
comfortRent_Mumbai = comfort_Mumbai %>% filter(Size == max(comfort_Mumbai$Size)) %>% summarise(Rent)

#combine the data of biggest houses in each city
analysis_size <- c(max(comfort_Bangalore$Size),rep(max(comfort_Chennai$Size),2),max(comfort_Delhi$Size),max(comfort_Hyderabad$Size),max(comfort_Kolkata$Size),max(comfort_Mumbai$Size))
#combine the data of price of biggest houses in each city
analysis_rent <- c(comfortRent_Bangalore,comfortRent_Chennai[1,1],comfortRent_Chennai[2,1],comfortRent_Delhi,comfortRent_Hyderabad,comfortRent_Kolkata,comfortRent_Mumbai)
#convert analysis_rent as integer
convert_rent <- as.integer(analysis_rent)
analysis_worth <- data.frame(dataOfBig_city,analysis_size,convert_rent)

ggplot(analysis_worth, aes(fill=analysis_size, y=convert_rent, x=dataOfBig_city)) + 
  geom_bar(position="dodge", stat="identity") + labs(title = "The value for money of houses in each city")
#-------------------------------------------------------------------------------------------
#Analysis 2 Analysis the number of BHK and bathroom in the biggest and cheapest in each city
#biggest house of bhk and bathroom
biggestBangalore_bhkBath = comfort_Bangalore %>% filter(Size == max(comfort_Bangalore$Size)) %>% summarise(BHK, Bathroom)

biggestChennai_bhkBath = comfort_Chennai %>% filter(Size == max(comfort_Chennai$Size)) %>% summarise(BHK, Bathroom)

biggestDelhi_bhkBath = comfort_Delhi %>% filter(Size == max(comfort_Delhi$Size)) %>% summarise(BHK, Bathroom)

biggestHyderabad_bhkBath = comfort_Hyderabad %>% filter(Size == max(comfort_Hyderabad$Size)) %>% summarise(BHK, Bathroom)

biggestKolkata_bhkBath = comfort_Kolkata %>% filter(Size == max(comfort_Kolkata$Size)) %>% summarise(BHK, Bathroom)

biggestMumbai_bhkBath = comfort_Mumbai %>% filter(Size == max(comfort_Mumbai$Size)) %>% summarise(BHK, Bathroom)

#the cheapest house of bhk and bathroom
cheapest_Bangalore_bhkBath = comfort_Bangalore %>% filter(Rent == min(comfort_Bangalore$Rent)) %>% summarise(BHK, Bathroom)

cheapest_ChennaibhkBath = comfort_Chennai %>% filter(Rent == min(comfort_Chennai$Rent)) %>% summarise(BHK, Bathroom)

cheapest_DelhibhkBath = comfort_Delhi %>% filter(Rent == min(comfort_Delhi$Rent)) %>% summarise(BHK, Bathroom)

cheapest_HyderabadbhkBath = comfort_Hyderabad %>% filter(Rent == min(comfort_Hyderabad$Rent)) %>% summarise(BHK, Bathroom)

cheapest_KolkatabhkBath = comfort_Kolkata %>% filter(Rent == min(comfort_Kolkata$Rent)) %>% summarise(BHK, Bathroom)

cheapest_MumbaibhkBath = comfort_Mumbai %>% filter(Rent == min(comfort_Mumbai$Rent)) %>% summarise(BHK, Bathroom)

#bar chart
dataOfBig_bhkAndBathroom <- c(biggestBangalore_bhkBath$BHK,biggestChennai_bhkBath$BHK,biggestDelhi_bhkBath$BHK,
                              biggestHyderabad_bhkBath$BHK,biggestKolkata_bhkBath$BHK,biggestMumbai_bhkBath$BHK,
                              biggestBangalore_bhkBath$Bathroom,biggestChennai_bhkBath$Bathroom,biggestDelhi_bhkBath$Bathroom,
                              biggestHyderabad_bhkBath$Bathroom,biggestKolkata_bhkBath$Bathroom,biggestMumbai_bhkBath$Bathroom)
dataOfBig_city <- c("Bangalore","Chennai(1)","Chennai(2)","Delhi","Hyderabad","Kolkata","Mumbai")
condition_big <- c(rep("BHK",7),rep("Bathroom",7))
combineBig <- data.frame(dataOfBig_city,condition_big,dataOfBig_bhkAndBathroom)
ggplot(combineBig, aes(fill=condition_big, y = dataOfBig_bhkAndBathroom, x = dataOfBig_city)) + 
  geom_bar(position="dodge", stat="identity") + labs(title = "The number of BHK and Bathroom of Biggest house in each City")

dataOfCheap_bhkAndBathroom <- c(cheapest_Bangalore_bhkBath$BHK,cheapest_ChennaibhkBath$BHK,cheapest_DelhibhkBath$BHK,
                                cheapest_HyderabadbhkBath$BHK,cheapest_KolkatabhkBath$BHK,cheapest_MumbaibhkBath$BHK,
                                cheapest_Bangalore_bhkBath$Bathroom,cheapest_ChennaibhkBath$Bathroom,cheapest_DelhibhkBath$Bathroom,
                                cheapest_HyderabadbhkBath$Bathroom,cheapest_KolkatabhkBath$Bathroom,cheapest_MumbaibhkBath$Bathroom)
dataOfCheap_city <- c("Bangalore","Chennai","Delhi","Hyderabad","Kolkata","Mumbai(1)","Mumbai(2)")
condition_cheap <- c(rep("BHK",7),rep("Bathroom",7))
combineCheap <- data.frame(dataOfCheap_city,condition_cheap,dataOfCheap_bhkAndBathroom)
ggplot(combineCheap, aes(fill=condition_cheap, y = dataOfCheap_bhkAndBathroom, x = dataOfCheap_city)) + 
  geom_bar(position="dodge", stat="identity") + labs(title = "The number of BHK and Bathroom of Cheapest in each City")

#-------------------------------------------------------------------------------------------
#Question 6 Which rent houses is friendly to bachelors
#Analysis 1 Analysis those houses for bachelors and with 1 BHK and bathroom
forBachelor = read_data %>% filter(Rent<=5000,BHK=="1",Bathroom=="1",Furnishing.Status=="Furnished") %>%
  summarise(Rent,Size,Area.Type,City,Area.Locality)
ggparcoord(forBachelor,showPoints = TRUE,columns = 1:4, groupColumn = 5,alphaLines = 1,
           title = "Houses with 1 BHK,1 Bathroom and cheap")+
  theme(axis.title.x=element_blank(),
        axis.title.y=element_blank()) +
  scale_x_discrete(expand = c(0.1,0.1))

#Analysis 2 Analysis the cheapest houses with different furnishing status
cheapestHouseUnf = read_data %>% filter(Rent<=1200,Furnishing.Status=="Furnished") %>%
  summarise(Rent,City,Furnishing.Status)
cheapestHouseSemif = read_data %>% filter(Rent<=1500,Furnishing.Status=="Semi-Furnished") %>%
  summarise(Rent,City,Furnishing.Status)
cheapestHouseFur = read_data %>% filter(Rent<=2000,Furnishing.Status=="Unfurnished") %>%
  summarise(Rent,City,Furnishing.Status)
combineCheapest = rbind(cheapestHouseUnf,cheapestHouseSemif,cheapestHouseFur)
ggplot(combineCheapest, aes(x=City, y=Rent,fill=Furnishing.Status)) + geom_col() +
  labs(title="Cheapest House With Different State of Furnishing Status")

#-------------------------------------------------------------------------------------------
#Question 7 Will the area types determine what king of tenant that the owner preferred in each city
#Analysis 1 Analysis the percentage of area type in different tenant type in Bangalore
KolBCarpet = nrow(read_data %>% filter(City == "Kolkata" & Tenant.Preferred == "Bachelors" &
                                                       Area.Type == "Carpet Area"))
KolBSuper = nrow(read_data %>% filter(City == "Kolkata" & Tenant.Preferred == "Bachelors" &
                                                      Area.Type == "Super Area"))
KolBFCarpet = nrow(read_data %>% filter(City == "Kolkata" & Tenant.Preferred == "Bachelors/Family" &
                                                         Area.Type == "Carpet Area"))
KolBFSuper = nrow(read_data %>% filter(City == "Kolkata" & Tenant.Preferred == "Bachelors/Family" &
                                                        Area.Type == "Super Area"))
KolFCarpet = nrow(read_data %>% filter(City == "Kolkata" & Tenant.Preferred == "Family" &
                                                       Area.Type == "Carpet Area"))
KolFSuper = nrow(read_data %>% filter(City == "Kolkata" & Tenant.Preferred == "Family" &
                                                      Area.Type == "Super Area"))
allTypeKol = c(KolBCarpet,KolBSuper,KolBFCarpet,KolBFSuper,KolFCarpet,KolFSuper)

percentOfKolkataArea = paste0(round(100*allTypeKol/sum(allTypeKol),2),"%")
tenantareatype = c("Bachelors(Carpet Area)","Bachelors(Super Area)","Bachelors/Family(Carpet Area)",
                   "Bachelors/Family(Super Area)","Family(Carpet Area)","Family(Super Area)")
pie3D(allTypeKol,labels=paste(percentOfKolkataArea),main="Percentage of Area Type in different tenant type in Kolkata"
      ,col = c("red","darkred","green","darkgreen","blue","darkblue"),theta=1,labelcex = 0.8)
legend("bottomleft", tenantareatype, cex = 0.7, fill=c("red","darkred","green","darkgreen","blue","darkblue"))

#Analysis 2 Analysis the percentage of area type in different tenant type in Chennai
MumBCarpet = nrow(read_data %>% filter(City == "Mumbai" & Tenant.Preferred == "Bachelors" &
                                                      Area.Type == "Carpet Area"))
MumBSuper = nrow(read_data %>% filter(City == "Mumbai" & Tenant.Preferred == "Bachelors" &
                                                     Area.Type == "Super Area"))
MumBFCarpet = nrow(read_data %>% filter(City == "Mumbai" & Tenant.Preferred == "Bachelors/Family" &
                                                        Area.Type == "Carpet Area"))
MumBFSuper = nrow(read_data %>% filter(City == "Mumbai" & Tenant.Preferred == "Bachelors/Family" &
                                                       Area.Type == "Super Area"))
MumFCarpet = nrow(read_data %>% filter(City == "Mumbai" & Tenant.Preferred == "Family" &
                                                      Area.Type == "Carpet Area"))
MumFSuper = nrow(read_data %>% filter(City == "Mumbai" & Tenant.Preferred == "Family" &
                                                     Area.Type == "Super Area"))
allTypeMumbai = c(MumBCarpet,MumBSuper,MumBFCarpet,MumBFSuper,MumFCarpet,MumFSuper)
percentOfMumbaiArea = paste0(round(100*allTypeMumbai/sum(allTypeMumbai),2),"%")
pie3D(allTypeMumbai,labels=paste(percentOfMumbaiArea),main="Percentage of Area Type in different tenant type in Mumbai"
      ,col = c("red","darkred","green","darkgreen","blue","darkblue"),theta=1,labelcex = 0.8)
legend("bottomleft", tenantareatype, cex = 0.6, fill=c("red","darkred","green","darkgreen","blue","darkblue"))

#Analysis 3 Analysis the percentage of area type in different tenant type in Delhi
BanBCarpet = nrow(read_data %>% filter(City == "Bangalore" & Tenant.Preferred == "Bachelors" &
                                                         Area.Type == "Carpet Area"))
BanBSuper = nrow(read_data %>% filter(City == "Bangalore" & Tenant.Preferred == "Bachelors" &
                                                        Area.Type == "Super Area"))
BanBFCarpet = nrow(read_data %>% filter(City == "Bangalore" & Tenant.Preferred == "Bachelors/Family" &
                                                           Area.Type == "Carpet Area"))
BanBFSuper = nrow(read_data %>% filter(City == "Bangalore" & Tenant.Preferred == "Bachelors/Family" &
                                                          Area.Type == "Super Area"))
BanFCarpet = nrow(read_data %>% filter(City == "Bangalore" & Tenant.Preferred == "Family" &
                                                         Area.Type == "Carpet Area"))
BanFSuper = nrow(read_data %>% filter(City == "Bangalore" & Tenant.Preferred == "Family" &
                                                        Area.Type == "Super Area"))
allTypeBangalore = c(BanBCarpet,BanBSuper,BanBFCarpet,BanBFSuper,BanFCarpet,BanFSuper)
percentOfBangaloreArea = paste0(round(100*allTypeBangalore/sum(allTypeBangalore),2),"%")
pie3D(allTypeBangalore,labels=paste(percentOfBangaloreArea),main="Percentage of Area Type in different tenant type in Bangalore"
      ,col = c("red","darkred","green","darkgreen","blue","darkblue"),theta=1,labelcex = 0.8)
legend("bottomright", tenantareatype, cex = 0.6, fill=c("red","darkred","green","darkgreen","blue","darkblue"))

#Analysis 4 Analysis the percentage of area type in different tenant type in Hyderabad
remove(allTypeChennai)

DelBCarpet = nrow(read_data %>% filter(City == "Delhi" & Tenant.Preferred == "Bachelors" &
                                                     Area.Type == "Carpet Area"))
DelBhuper = nrow(read_data %>% filter(City == "Delhi" & Tenant.Preferred == "Bachelors" &
                                                    Area.Type == "Super Area"))
DelBFCarpet = nrow(read_data %>% filter(City == "Delhi" & Tenant.Preferred == "Bachelors/Family" &
                                                       Area.Type == "Carpet Area"))
DelBFSuper = nrow(read_data %>% filter(City == "Delhi" & Tenant.Preferred == "Bachelors/Family" &
                                                      Area.Type == "Super Area"))
DelFCarpet = nrow(read_data %>% filter(City == "Delhi" & Tenant.Preferred == "Family" &
                                                     Area.Type == "Carpet Area"))
DelFSuper = nrow(read_data %>% filter(City == "Delhi" & Tenant.Preferred == "Family" &
                                                    Area.Type == "Super Area"))
allTypeDelhi = c(DelBCarpet,DelBhuper,DelBFCarpet,DelBFSuper,DelFCarpet,DelFSuper)
percentOfDelhiArea = paste0(round(100*allTypeDelhi/sum(allTypeDelhi),2),"%")
pie3D(allTypeDelhi,labels=paste(percentOfDelhiArea),main="Percentage of Area Type in different tenant type in Delhi"
      ,col = c("red","darkred","green","darkgreen","blue","darkblue"),theta=1,labelcex = 0.8)
legend("bottomleft", tenantareatype, cex = 0.6, fill=c("red","darkred","green","darkgreen","blue","darkblue"))

#Analysis 5 Analysis the percentage of area type in different tenant type in Kolkata
CheBCarpet = nrow(read_data %>% filter(City == "Chennai" & Tenant.Preferred == "Bachelors" &
                                                       Area.Type == "Carpet Area"))
CheBSuper = nrow(read_data %>% filter(City == "Chennai" & Tenant.Preferred == "Bachelors" &
                                                      Area.Type == "Super Area"))
CheBFCarpet = nrow(read_data %>% filter(City == "Chennai" & Tenant.Preferred == "Bachelors/Family" &
                                                         Area.Type == "Carpet Area"))
CheBFSuper = nrow(read_data %>% filter(City == "Chennai" & Tenant.Preferred == "Bachelors/Family" &
                                                        Area.Type == "Super Area"))
CheFCarpet = nrow(read_data %>% filter(City == "Chennai" & Tenant.Preferred == "Family" &
                                                       Area.Type == "Carpet Area"))
CheFSuper = nrow(read_data %>% filter(City == "Chennai" & Tenant.Preferred == "Family" &
                                                      Area.Type == "Super Area"))
allTypeChe = c(CheBCarpet,CheBSuper,CheBFCarpet,CheBFSuper,CheFCarpet,CheFSuper)
percentOfChennaiArea = paste0(round(100*allTypeChe/sum(allTypeChe),2),"%")
pie3D(allTypeChe,labels=paste(percentOfChennaiArea),main="Percentage of Area Type in different tenant type in Chennai"
      ,col = c("red","darkred","green","darkgreen","blue","darkblue"),theta=1,labelcex = 0.8)
legend("bottom", tenantareatype, cex = 0.6, fill=c("red","darkred","green","darkgreen","blue","darkblue"))

#Analysis 6 Analysis the percentage of area type in different tenant type in Mumbai
HydBCarpet = nrow(read_data %>% filter(City == "Hyderabad" & Tenant.Preferred == "Bachelors" &
                                                         Area.Type == "Carpet Area"))
HydBSuper = nrow(read_data %>% filter(City == "Hyderabad" & Tenant.Preferred == "Bachelors" &
                                                        Area.Type == "Super Area"))
HydBFCarpet = nrow(read_data %>% filter(City == "Hyderabad" & Tenant.Preferred == "Bachelors/Family" &
                                                           Area.Type == "Carpet Area"))
HydBFSuper = nrow(read_data %>% filter(City == "Hyderabad" & Tenant.Preferred == "Bachelors/Family" &
                                                          Area.Type == "Super Area"))
HydFCarpet = nrow(read_data %>% filter(City == "Hyderabad" & Tenant.Preferred == "Family" &
                                                         Area.Type == "Carpet Area"))
HydFSuper = nrow(read_data %>% filter(City == "Hyderabad" & Tenant.Preferred == "Family" &
                                                        Area.Type == "Super Area"))
allTypeHyd = c(HydBCarpet,HydBSuper,HydBFCarpet,HydBFSuper,HydFCarpet,HydFSuper)
percentOfHyderabadArea = paste0(round(100*allTypeHyd/sum(allTypeHyd),2),"%")
pie3D(allTypeHyd,labels=paste(percentOfHyderabadArea),main="Percentage of Area Type in different tenant type in Hyderabad"
      ,col = c("red","darkred","green","darkgreen","blue","darkblue"),theta=1,labelcex = 0.8)
legend("bottom", tenantareatype, cex = 0.6, fill=c("red","darkred","green","darkgreen","blue","darkblue"))

#-------------------------------------------------------------------------------------------
#Question 8 Will the furnishing status determine what kind of tenant the owner will preferred in each city
#Analysis 1 Analysis the percentage of furnishing status in different tenant type in Bangalore
Kol_B_Un = nrow(read_data %>% filter(City == "Kolkata" & Tenant.Preferred == "Bachelors" & Furnishing.Status == "Unfurnished"))

Kol_B_Semi = nrow(read_data %>% filter(City == "Kolkata" & Tenant.Preferred == "Bachelors" & Furnishing.Status == "Semi-Furnished"))

Kol_B_F = nrow(read_data %>% filter(City == "Kolkata" & Tenant.Preferred == "Bachelors" & Furnishing.Status == "Furnished"))

Kol_BF_Un = nrow(read_data %>% filter(City == "Kolkata" & Tenant.Preferred == "Bachelors/Family" & Furnishing.Status == "Unfurnished"))

Kol_BF_Semi = nrow(read_data %>% filter(City == "Kolkata" & Tenant.Preferred == "Bachelors/Family" & Furnishing.Status == "Semi-Furnished"))

Kol_BF_F = nrow(read_data %>% filter(City == "Kolkata" & Tenant.Preferred == "Bachelors/Family" & Furnishing.Status == "Furnished"))

Kol_F_Un = nrow(read_data %>% filter(City == "Kolkata" & Tenant.Preferred == "Family" & Furnishing.Status == "Unfurnished"))

Kol_F_Semi = nrow(read_data %>% filter(City == "Kolkata" & Tenant.Preferred == "Family" & Furnishing.Status == "Semi-Furnished"))

Kol_F_F = nrow(read_data %>% filter(City == "Kolkata" & Tenant.Preferred == "Family" & Furnishing.Status == "Furnished"))

combineKolkata = c(Kol_B_Un,Kol_B_Semi,Kol_B_F,Kol_BF_Un,Kol_BF_Semi,Kol_BF_F,Kol_F_Un,Kol_F_Semi,Kol_F_F)
#convert to percentage
percentOfKolkata = paste0(round(100*combineKolkata/sum(combineKolkata),2),"%")

tenantTypeStatus = c("B(Unfurnished)","B(Semi-Furnished)","B(Furnished)","B/Family(Unfurnished)",
                            "B/F(Semi-Furnished)","B/F(Furnished)","F(Unfurnished)","F(Semi-Furnished)","F(Furnished)")

pie3D(combineKolkata,labels = paste(percentOfKolkata),main = "Percentage of Furnishing Status in Different Tenant Type in Kolkata"
      ,col = c("red","orange", "yellow","green","blue","purple","blue3","grey","#F4A582"),theta = 1,labelcex = 0.7)
legend("bottomleft", tenantTypeStatus, cex = 0.5, fill=c("red","orange", "yellow","green","blue","purple","blue3","grey","#F4A582"))

#Analysis 2 Analysis the percentage of furnishing status in different tenant type in Chennai
Mum_B_Un = nrow(read_data %>% filter(City == "Mumbai" & Tenant.Preferred == "Bachelors" & Furnishing.Status == "Unfurnished"))

Mum_B_Semi = nrow(read_data %>% filter(City == "Mumbai" & Tenant.Preferred == "Bachelors" & Furnishing.Status == "Semi-Furnished"))

Mum_B_F = nrow(read_data %>% filter(City == "Mumbai" & Tenant.Preferred == "Bachelors" & Furnishing.Status == "Furnished"))

Mum_BF_Un = nrow(read_data %>% filter(City == "Mumbai" & Tenant.Preferred == "Bachelors/Family" & Furnishing.Status == "Unfurnished"))

Mum_BF_Semi = nrow(read_data %>% filter(City == "Mumbai" & Tenant.Preferred == "Bachelors/Family" & Furnishing.Status == "Semi-Furnished"))

Mum_BF_F = nrow(read_data %>% filter(City == "Mumbai" & Tenant.Preferred == "Bachelors/Family" & Furnishing.Status == "Furnished"))

Mum_F_Un = nrow(read_data %>% filter(City == "Mumbai" & Tenant.Preferred == "Family" & Furnishing.Status == "Unfurnished"))

Mum_F_Semi = nrow(read_data %>% filter(City == "Mumbai" & Tenant.Preferred == "Family" & Furnishing.Status == "Semi-Furnished"))

Mum_F_F = nrow(read_data %>% filter(City == "Mumbai" & Tenant.Preferred == "Family" & Furnishing.Status == "Furnished"))

combineMumbai = c(Mum_B_Un, Mum_B_Semi, Mum_B_F, Mum_BF_Un, Mum_BF_Semi, Mum_BF_F, Mum_F_Un, Mum_F_Semi, Mum_F_F)

#convert to percentage
percentOfMumbai = paste0(round(100*combineMumbai/sum(combineMumbai),2),"%")

pie3D(combineMumbai,labels=paste(percentOfMumbai),    
      radius=0.75,main="Percentage of Furnishing Status in Different Tenant Type in Mumbai"
      ,col = c("red","orange", "yellow","green","blue","purple","blue3","grey","#F4A582"),theta = 1,labelcex = 1)
legend("topleft", tenantTypeStatus, cex = 0.6, fill=c("red","orange", "yellow","green","blue","purple","blue3","grey","#F4A582"))

#Analysis 3 Analysis the percentage of furnishing status in different tenant type in Delhi
Ban_B_Un = nrow(read_data %>% filter(City == "Bangalore" & Tenant.Preferred == "Bachelors" &Furnishing.Status == "Unfurnished"))

Ban_B_Semi = nrow(read_data %>% filter(City == "Bangalore" & Tenant.Preferred == "Bachelors" &Furnishing.Status == "Semi-Furnished"))

Ban_B_F = nrow(read_data %>% filter(City == "Bangalore" & Tenant.Preferred == "Bachelors" &Furnishing.Status == "Furnished"))

Ban_BF_Un = nrow(read_data %>% filter(City == "Bangalore" & Tenant.Preferred == "Bachelors/Family" &Furnishing.Status == "Unfurnished"))

Ban_BF_Semi = nrow(read_data %>% filter(City == "Bangalore" & Tenant.Preferred == "Bachelors/Family" &Furnishing.Status == "Semi-Furnished"))

Ban_BF_F = nrow(read_data %>% filter(City == "Bangalore" & Tenant.Preferred == "Bachelors/Family" &Furnishing.Status == "Furnished"))

Ban_F_Un = nrow(read_data %>% filter(City == "Bangalore" & Tenant.Preferred == "Family" &Furnishing.Status == "Unfurnished"))

Ban_F_Semi = nrow(read_data %>% filter(City == "Bangalore" & Tenant.Preferred == "Family" &Furnishing.Status == "Semi-Furnished"))

Ban_F_F = nrow(read_data %>% filter(City == "Bangalore" & Tenant.Preferred == "Family" &Furnishing.Status == "Furnished"))

combineBangalore = c(Ban_B_Un,Ban_B_Semi,Ban_B_F,Ban_BF_Un,Ban_BF_Semi,Ban_BF_F,Ban_F_Un,Ban_F_Semi,Ban_F_F)

#convert to percentage
percentOfBangalore = paste0(round(100*combineBangalore/sum(combineBangalore),2),"%")

pie3D(combineBangalore,labels=paste(percentOfBangalore)
      ,main="Percentage of Furnishing Status in Different Tenant Type in Bangalore"
      ,col = c("red","orange", "yellow","green","blue","purple","blue3","grey","#F4A582"),theta = 1,labelcex = 0.8)
legend("topleft", tenantTypeStatus, cex = 0.8, fill=c("red","orange", "yellow","green","blue","purple","blue3","grey","#F4A582"))

#Analysis 4 Analysis the percentage of furnishing status in different tenant type in Hyderabad
Del_B_Un = nrow(read_data %>% filter(City == "Delhi" & Tenant.Preferred == "Bachelors" & Furnishing.Status == "Unfurnished"))

Del_B_Semi = nrow(read_data %>% filter(City == "Delhi" & Tenant.Preferred == "Bachelors" & Furnishing.Status == "Semi-Furnished"))

Del_B_F = nrow(read_data %>% filter(City == "Delhi" & Tenant.Preferred == "Bachelors" & Furnishing.Status == "Furnished"))

Del_BF_Un = nrow(read_data %>% filter(City == "Delhi" & Tenant.Preferred == "Bachelors/Family" & Furnishing.Status == "Unfurnished"))

Del_BF_Semi = nrow(read_data %>% filter(City == "Delhi" & Tenant.Preferred == "Bachelors/Family" & Furnishing.Status == "Semi-Furnished"))

Del_BF_F = nrow(read_data %>% filter(City == "Delhi" & Tenant.Preferred == "Bachelors/Family" & Furnishing.Status == "Furnished"))

Del_F_Un = nrow(read_data %>% filter(City == "Delhi" & Tenant.Preferred == "Family" & Furnishing.Status == "Unfurnished"))

Del_F_Semi = nrow(read_data %>% filter(City == "Delhi" & Tenant.Preferred == "Family" & Furnishing.Status == "Semi-Furnished"))

Del_F_F = nrow(read_data %>% filter(City == "Delhi" & Tenant.Preferred == "Family" & Furnishing.Status == "Furnished"))

CombineDelhi = c(Del_B_Un,Del_B_Semi,Del_B_F,Del_BF_Un,Del_BF_Semi,Del_BF_F,Del_F_Un,Del_F_Semi)

#convert to percentage
percentOfDelhi = paste0(round(100*CombineDelhi/sum(CombineDelhi),2),"%")

pie3D(CombineDelhi,labels = paste(percentOfDelhi),  
      radius = 1,main ="Percentage of Furnishing Status in Different Tenant Type in Delhi"
      ,col = c("red","orange", "yellow","green","blue","purple","grey","#F4A582"),theta = 1,labelcex = 0.8)
legend("center", tenantTypeStatus, cex = 0.6, fill=c("red","orange", "yellow","green","blue","purple","grey","#F4A582"))

#Analysis 5 Analysis the percentage of furnishing status in different tenant type in Kolkata
Che_B_Un = nrow(read_data %>% filter(City == "Chennai" & Tenant.Preferred == "Bachelors" & Furnishing.Status == "Unfurnished"))

Che_B_Semi = nrow(read_data %>% filter(City == "Chennai" & Tenant.Preferred == "Bachelors" & Furnishing.Status == "Semi-Furnished"))

Che_B_F = nrow(read_data %>% filter(City == "Chennai" & Tenant.Preferred == "Bachelors" & Furnishing.Status == "Furnished"))

Che_BF_Un = nrow(read_data %>% filter(City == "Chennai" & Tenant.Preferred == "Bachelors/Family" & Furnishing.Status == "Unfurnished"))

Che_BF_Semi = nrow(read_data %>% filter(City == "Chennai" & Tenant.Preferred == "Bachelors/Family" & Furnishing.Status == "Semi-Furnished"))

Che_BF_F = nrow(read_data %>% filter(City == "Chennai" & Tenant.Preferred == "Bachelors/Family" & Furnishing.Status == "Furnished"))

Che_F_Un = nrow(read_data %>% filter(City == "Chennai" & Tenant.Preferred == "Family" & Furnishing.Status == "Unfurnished"))

Che_F_Semi = nrow(read_data %>% filter(City == "Chennai" & Tenant.Preferred == "Family" & Furnishing.Status == "Semi-Furnished"))

Che_F_F = nrow(read_data %>% filter(City == "Chennai" & Tenant.Preferred == "Family" & Furnishing.Status == "Furnished"))

combineChennai = c(Che_B_Un,Che_B_Semi,Che_B_F,Che_BF_Un,Che_BF_Semi,Che_BF_F,Che_F_Un,Che_F_Semi,Che_F_F)

percentOfChennai = paste0(round(100*combineChennai/sum(combineChennai),2),"%")

pie3D(combineChennai,labels=paste(percentOfChennai),  
      main="Percentage of Furnishing Status in Different Tenant Type in Chennai"
      ,col = c("red","orange", "yellow","green","blue","purple","blue3","grey","#F4A582"),theta=0.4,labelcex = 0.8)
legend("topright", tenantTypeStatus, cex = 0.55, fill=c("red","orange", "yellow","green","blue","purple","blue3","grey","#F4A582"))

#Analysis 6 Analysis the percentage of furnishing status in different tenant type in Mumbai
Hyd_B_Un = nrow(read_data %>% filter(City == "Hyderabad" & Tenant.Preferred == "Bachelors" & Furnishing.Status == "Unfurnished"))

Hyd_B_Semi = nrow(read_data %>% filter(City == "Hyderabad" & Tenant.Preferred == "Bachelors" & Furnishing.Status == "Semi-Furnished"))

Hyd_B_F = nrow(read_data %>% filter(City == "Hyderabad" & Tenant.Preferred == "Bachelors" & Furnishing.Status == "Furnished"))

Hyd_BF_Un = nrow(read_data %>% filter(City == "Hyderabad" & Tenant.Preferred == "Bachelors/Family" & Furnishing.Status == "Unfurnished"))

Hyd_BF_Semi = nrow(read_data %>% filter(City == "Hyderabad" & Tenant.Preferred == "Bachelors/Family" & Furnishing.Status == "Semi-Furnished"))

Hyd_BF_F = nrow(read_data %>% filter(City == "Hyderabad" & Tenant.Preferred == "Bachelors/Family" & Furnishing.Status == "Furnished"))

Hyd_F_Un = nrow(read_data %>% filter(City == "Hyderabad" & Tenant.Preferred == "Family" & Furnishing.Status == "Unfurnished"))

Hyd_F_Semi = nrow(read_data %>% filter(City == "Hyderabad" & Tenant.Preferred == "Family" & Furnishing.Status == "Semi-Furnished"))

Hyd_F_F = nrow(read_data %>% filter(City == "Hyderabad" & Tenant.Preferred == "Family" & Furnishing.Status == "Furnished"))

combineHyderabad = c(Hyd_B_Un,Hyd_B_Semi,Hyd_B_F,Hyd_BF_Un,Hyd_BF_Semi,Hyd_BF_F,Hyd_F_Un,Hyd_F_Semi,Hyd_F_F)

percentOfHyderabad = paste0(round(100*combineHyderabad/sum(combineHyderabad),2),"%")

pie3D(combineHyderabad,labels=paste(percentOfHyderabad),  
      main="Percentage of Furnishing Status in Different Tenant Type in Hyderabad"
      ,col = c("red","orange", "yellow","green","blue","purple","blue3","grey","#F4A582"),theta=0.4,labelcex = 0.8)
legend("topright", tenantTypeStatus, cex = 0.6, fill=c("red","orange", "yellow","green","blue","purple","blue3","grey","#F4A582"))
