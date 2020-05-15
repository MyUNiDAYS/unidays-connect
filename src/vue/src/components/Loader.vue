<template>
    <div class="loader-wrapper">
        <div class="loader">
            <h2>{{ loaderText }}</h2>
            <div class="lds-ellipsis">
                <div></div>
                <div></div>
                <div></div>
                <div></div>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    props: ["loaderText"]
};
</script>
<style lang="sass" scoped>
.loader-wrapper
  left: 50%
  position: fixed
  background: rgba(70, 70, 70, 0.8)
  top: 0
  width: 120%
  transform: translateX(-50%)
  height: 100%
  z-index: 1000
  display: flex
  align-items: center
  justify-content: center
.loader
  display: flex
  align-items: center
  justify-content: center
h2
  margin-bottom: 10px
  padding-right: 10px
  color: #fff
.lds-ellipsis
	display: inline-block
	position: relative
	width: 80px
	height: 80px
	div
		position: absolute
		top: 33px
		width: 13px
		height: 13px
		border-radius: 50%
		background: #fff
		animation-timing-function: cubic-bezier(0, 1, 1, 0)
		&:nth-child(1)
			left: 8px
			animation: lds-ellipsis1 0.6s infinite
		&:nth-child(2)
			left: 8px
			animation: lds-ellipsis2 0.6s infinite
		&:nth-child(3)
			left: 32px
			animation: lds-ellipsis2 0.6s infinite
		&:nth-child(4)
			left: 56px
			animation: lds-ellipsis3 0.6s infinite
@keyframes lds-ellipsis1
  0%
    transform: scale(0)
  100%
    transform: scale(1)

@keyframes lds-ellipsis2
  0%
    transform: translate(0, 0)
  100%
    transform: translate(24px, 0)

@keyframes lds-ellipsis3
  0%
    transform: scale(1)
  100%
    transform: scale(0)
</style>
