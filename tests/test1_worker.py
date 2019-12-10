from selenium import webdriver
from selenium.webdriver.support.select import Select
from selenium.webdriver.common.keys import Keys
import time
driver = webdriver.Chrome()
driver.get(url="localhost:8080")
time.sleep(3)
element = driver.find_element_by_name("email")
element.clear()
element.send_keys("richard@gmail.com")
element = driver.find_element_by_name("pass")
element.clear()
element.send_keys("iamrichard")
element = driver.find_element_by_name("submit")
element.click()
time.sleep(5)
alert = driver.switch_to.alert
time.sleep(1)
alert.accept()
time.sleep(3)
element = driver.find_element_by_name("email")
element.clear()
element.send_keys("richar@gmail.com")
element = driver.find_element_by_name("pass")
element.clear()
element.send_keys("iamrichard")
element = driver.find_element_by_name("submit")
element.click()
time.sleep(3)
element = driver.find_element_by_xpath("/html/body/div/main[@class='container']/div[@class='menu']/div["
                                       "@class='option'][4]/a")
element.click()
time.sleep(2)
element = driver.find_element_by_xpath("/html/body/div/main[@class='container']/div[2]/div/div[1]/div["
                                       "@class='color-changer']/div[@class='main']/div[1]/div[@class='name'][2]")
element.click()
time.sleep(2)
alert = driver.switch_to.alert
time.sleep(2)
alert.send_keys("https://ru.wikipedia.org/wiki/Ford_Mustang")
time.sleep(2)
alert.accept()
time.sleep(5)
element = driver.find_element_by_xpath("/html/body/div/main[@class='container']/div[@class='menu']/div["
                                       "@class='option'][8]/a")
element.click()
time.sleep(2)
alert = driver.switch_to.alert
time.sleep(1)
alert.accept()
time.sleep(3)
driver.close()
