 const accountbtn = document.querySelector('#account-form')
 const accountclose = document.querySelector('#account-close')
 const rightbtn = document.querySelector('.fa-chevron-right')  
 const leftbtn = document.querySelector('.fa-chevron-left')  
 const imgNuber = document.querySelectorAll('.Slide-content-left-top img')
 const imgNuberLi = document.querySelectorAll('.Slide-content-left-bottom li')
 let index = 0
 accountbtn.addEventListener("click", function(){
    document.querySelector('.account-form').style.display = "flex"
 })
 accountclose.addEventListener("click", function(){
    document.querySelector('.account-form').style.display = "none"
 })
 rightbtn.addEventListener ("click", function(){
   index = index+1
   if(index>imgNuber.length-1){
      index=0
   }
   removeactive ()
   document.querySelector(".Slide-content-left-top").style.right = index * 100 +"%"
   imgNuberLi[index].classList.add("active")
 })
 leftbtn.addEventListener ("click", function(){
   index = index-1
   if(index<0){
      index=imgNuber.length-1
   }
   removeactive ()
   document.querySelector(".Slide-content-left-top").style.right = index * 100 +"%"
   imgNuberLi[index].classList.add("active")
 })
 imgNuberLi.forEach(function(image, index){
   image.addEventListener("click", function(){
      removeactive ()
      document.querySelector(".Slide-content-left-top").style.right = index * 100 +"%" 
      image.classList.add("active")
   })
 })
 function removeactive (){
   let imgactive = document.querySelector('.active')
   imgactive.classList.remove("active")
 }
 function imgauto (){
   index = index + 1
   if(index>imgNuber.length-1){
      index=0
   }
   removeactive ()
   document.querySelector(".Slide-content-left-top").style.right = index * 100 +"%" 
   imgNuberLi[index].classList.add("active")
 }
 setInterval(imgauto,5000)
 //-----------SlideProduct--------
 const rightbtn2 = document.querySelector('.fa-chevron-right-2')  
 const leftbtn2 = document.querySelector('.fa-chevron-left-2')  
 const imgNuber2 = document.querySelectorAll('.Slide-product-content-items')
 rightbtn2.addEventListener ("click", function(){
   index = index+1
   if(index>imgNuber2.length-1){
      index=0
   }
   document.querySelector(".Slide-product-content-items-content").style.right = index * 100 +"%"
})
leftbtn2.addEventListener ("click", function(){
   index = index-1
   if(index<0){
      index=imgNuber2.length-1
   }
   document.querySelector(".Slide-product-content-items-content").style.right = index * 100 +"%"
})

