# Create the HorizontalPodAutoscaler
#See https://kubernetes.io/docs/tasks/run-application/horizontal-pod-autoscale-walkthrough/

write-host "1. Moved control to correct folder"
cd C:\D\msc_project\msc-project\experiments\vpa_native_default_scale_down

write-host "2. Applying deployment"
kubectl apply -f deployment.yml

write-host "3. Applying service"
kubectl apply -f service.yml

write-host "4. Applying service monitor"
kubectl apply -f servicemonitor.yml


if (-not(Test-Path "c:\temp\autoscaler"))
{
	write-host "5. Downloading vertical scaler source code"
	cd "c:\temp\autoscaler"
	git clone https://github.com/kubernetes/autoscaler.git
}
else
{
	write-host "5. Vertical scaler source code already downloaded, dont need to download again ..."
}

write-host "6. Port forward for appropriate applications and ports: start-job {kubectl port-forward deploy/nginx-deployment 8001:80}"

write-host "7.  Finished setup"