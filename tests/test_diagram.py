from selenium import webdriver
from selenium.webdriver.common.keys import Keys
import time
driver = webdriver.Chrome()
driver.get(url="localhost:8080")
time.sleep(5)
element = driver.find_element_by_name("email")
element.clear()
element.send_keys("james@gmail.com")
element = driver.find_element_by_name("pass")
element.clear()
element.send_keys("iamjames")
element = driver.find_element_by_name("submit")
element.click()
time.sleep(3)
element = driver.find_element_by_xpath("/html/body/div/main[@class='container']/div[2]/div[1]/div["
                                       "@class='color-changer']/div[@class='main']/div[@class='name'][3]")
element.click()
time.sleep(2)
element = driver.find_element_by_xpath("/html/body/div/main[@class='container']/div[@class='menu']/div["
                                       "@class='option'][6]/a")
element.click()
time.sleep(2)
element = driver.find_element_by_xpath("/html/body/div/main[@class='container']/div[2]/label[1]/input")
element.click()
element.send_keys(Keys.ARROW_LEFT)
element.send_keys(Keys.ARROW_LEFT)
element.send_keys("10052019")
element = driver.find_element_by_xpath("/html/body/div/main[@class='container']/div[2]/label[2]/input")
element.click()
element.send_keys(Keys.ARROW_LEFT)
element.send_keys(Keys.ARROW_LEFT)
element.send_keys("18052019")
element = driver.find_element_by_xpath("/html/body/div/main[@class='container']/div[2]/input")
element.click()
time.sleep(10)
element = driver.find_element_by_xpath("/html/body/div/main[@class='container']/div[@class='menu']/div["
                                       "@class='option'][8]/a")
element.click()
time.sleep(2)
alert = driver.switch_to.alert
time.sleep(2)
alert.accept()
time.sleep(3)
driver.close()



