import torch
import torchvision
from torchvision import models
import torchvision.transforms as T
import numpy as np
from PIL import Image
import matplotlib.pyplot as plt
import urllib.request
import urllib
import cv2 as cv 
import os

class Caclulating:
    def __init__(self,input_file_path,output_file_path,imglist):
        #print(imglist)
        print(input_file_path)

        self.imglist = imglist
        self.input_file_path = input_file_path
        self.output_file_path = output_file_path
        #print(self.input_file_path+self.imglist+'.jpg')

    def cal(self):
        os.environ['KMP_DUPLICATE_LIB_OK']='True'

        #print(self.input_file_path+self.imglist[0])
        

        #print('pytorch', torch.__version__)
        #print('torchvision', torchvision.__version__)

        #2(todo)
        COLORS = np.array([
            (0, 0, 0),       # 0=background
            (128, 0, 0),     # 1=aeroplane
            (0, 128, 0),     # 2=bicycle
            (128, 128, 0),   # 3=bird
            (0, 0, 128),     # 4=boat
            (128, 0, 128),   # 5=bottle
            (0, 128, 128),   # 6=bus
            (128, 128, 128), # 7=car
            (255, 255, 255), # 8=cat
            (192, 0, 0),     # 9=chair
            (64, 128, 0),    # 10=cow
            (192, 128, 0),   # 11=dining table
            (64, 0, 128),    # 12=dog
            (192, 0, 128),   # 13=horse
            (64, 128, 128),  # 14=motorbike
            (192, 128, 128), # 15=person
            (0, 64, 0),      # 16=potted plant
            (128, 64, 0),    # 17=sheep
            (0, 192, 0),     # 18=sofa
            (128, 192, 0),   # 19=train
            (0, 64, 128)     # 20=tv/monitor    

        ])
        #표시할 레이블을 20개 이상 잡을 필요는 없을듯(구분 힘듬,가시성 x)

        # Download Model

        deeplab = models.segmentation.deeplabv3_resnet101(pretrained=True).eval()

        # Load Image
        for i in self.imglist: 
            img_name = i[:-4] 
            print(img_name)
            img= Image.open(self.input_file_path+i)# + png <____--_______________-여기에 이미지 불러올 경로 인가하면 됨. 


            type(img)
            print('-load image from Local_path-')

            #plt.figure(figsize=(16, 16)) # matplotlib에서 보여줄 때의 사이즈 지정.
            #plt.imshow(img)

            trf = T.Compose([
                T.ToTensor(), 
                T.Normalize(
                    mean=[0.485, 0.456, 0.406],
                    std=[0.229, 0.224, 0.225]
                )
            ])

            print(trf(img).shape)

            input_img = trf(img).unsqueeze(0)
            #trf(img)는 img를 텐서로 변환하고 정규화까지 거친 상태의 torch.tensor이며 torch.unsqueeze()는 해당 텐서에 차원 크기를 1 추가해준다.

            print(input_img.shape)


            #결과적으로 deeplab()에 파라미터로 주어질 input_img는 torch.tensor고 shape는 torch.Size([1, 3, 780, 1170])로 출력됨.


            #(done)3  move the input and model to GPU for speed if available
            if torch.cuda.is_available():
                input_img = input_img.to('cuda')
                deeplab.to('cuda')

            #Inference


            out = deeplab(input_img)['out']

            print(out.shape)

            out = torch.argmax(out.squeeze(), dim=0) #여기까진  out이 torch.tensor임
            out = out.detach().cpu().numpy()        #이 구문 실행후 out은 numpy.ndarray임.
                                                #근데 왜 fromarray()가 안될까
            print(out.shape)
            print(np.unique(out))

            img_cv = np.array(out)
            print(self.output_file_path+'\\'+img_name+'_gray_img.png')
            cv.imwrite(self.output_file_path+'\\'+ img_name+'_gray_img.png', img_cv)
            #test_img = cv.imread('gray_img.jpg',0)



