<template>
  <div id="app">
    <img alt="Vue logo" src="./assets/logo.png" style="width: 50px;">
    <h1>Image Explorer</h1>
	<h2><label>Type tags to search</label></h2>
    <input @keyup.enter="search" v-model="tags" />
    <button :disabled="tags.length < 1" @click="search">Search images</button>    
    <div class="section-flickr">
		<h3 v-if="errorMsg.length > 0">{{ errorMsg }}</h3>
      <ul class="flickr-showcase">
        <li v-for="url in urls" :key="url">
          <figure class="flickr-photo">
            <img @click="openImage(url)" :src="url" style="" >
          </figure>
        </li>
      </ul>
    </div>
  </div>
</template>

<script>
import axios from 'axios'

export default {
	name: 'App',
	components: {
	},
	data() {
		return {
			data: "",
			photos: "",
			urls: [],
			errors: [],
			tags: "",
			errorMsg: ""
		}
	},
	created() {		
	},
	mounted() {
	},
	methods: {
		search() {
			this.urls = "";
			if (this.tags.length > 0)
				this.GetImages()
		},		
		GetImages() {
			console.log("Searching for:", this.tags)
			this.errorMsg = ""
			axios.post('http://localhost:5000/Image', {
				headers: {}
			}, {
				params: {
					tags: this.tags
				}
			})
				.then(response => {
					var data = response.data
					this.data = JSON.parse(data.substring(14, data.length - 1))
					this.photos = this.data.photos["photo"]
					this.PrepareURL()
				})
				.catch(e => {
					this.errors.push(e)
					this.error()
				})
		},
		PrepareURL() {
			const urls = []
			for (const [, photo] of Object.entries(this.photos)) {
				if (photo['farm'] > 0 && photo['server'] > 0 && photo['id'] > 0) {
					var url = 'https://farm{farm-id}.staticflickr.com/{server-id}/{id}_{secret}.png'
					url = url.replace(/{farm-id}/, photo['farm'])
					url = url.replace(/{server-id}/, photo['server'])
					url = url.replace(/{id}/, photo['id'])
					url = url.replace(/{secret}/, photo['secret'])
					urls.push(url)
				}
			}
			this.urls = urls
		},
		openImage(url) {
			window.open(url, "_blank")
		},
		error() {
			this.urls = ""
			this.errorMsg = "Error in finding images for  {this.tags} !!!"
			console.log(this.errorMsg)
		}
	},
}
</script>

<style>
#app {
	font-family: Arial, sans-serif;
	-webkit-font-smoothing: antialiased;	
	text-align: center;
	color: #101113;
	margin-top: 60px;
	min-width: 320px;
}

ul {
	list-style-type: none;
	padding: 0;
}

li {
	display: inline-block;
}

.flickr-photo {
	transform: scale(1);
	width: 100%;
	margin: 0;
	overflow: hidden;
	background-color: #000;
	vertical-align: middle;
}
.flickr-photo img:hover {
	transform: scale(1.5);
	opacity: 1;
}
</style>
